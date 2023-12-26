using System;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Tasq.Interfaces;
using Tasq.Models;
using Tasq.ViewModels;

namespace Tasq.Controllers
{
	public class TareaController : Controller
	{
        private readonly ITareaRepository _tareaR; // ya no se nececita eso de _context porque ya eso está hecho con la Interface y los Repositories
        private readonly IDepartamentoRepository _depaR; // ya no se nececita eso de _context porque ya eso está hecho con la Interface y los Repositories
        private readonly ISedeRepository _sedeR; // ya no se nececita eso de _context porque ya eso está hecho con la Interface y los Repositories
        private readonly IHttpContextAccessor _httpCA; // Para regresar lo del AppUserId en el form de Create

        public TareaController(ITareaRepository tareaR, IDepartamentoRepository depaR)
		{
            _tareaR = tareaR;
            _depaR = depaR;
		}


        public async Task<IActionResult> Index()
        {
            IEnumerable<Tarea> tarea = await _tareaR.GetAll();
            return View(tarea);
        }



        // [Authorize(Roles = "Admin")]
        [HttpGet("tarea/create/{idDepartamento}")]
        public async Task<IActionResult> Create(int idDepartamento)
        {
            Departamento depa = await _depaR.GetByIdAsync(idDepartamento);

            if (depa == null) return View("Error");


            var datos = new CreateTareaVM
            {
                NombreDepartamento = depa.Nombre,
                IdDepartamento = depa.Id,

            };

            return View(datos);
        }


        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTareaVM tareaVM)
        {
            if (!ModelState.IsValid) // Eso de ModelState es una clase en donde cuando postemaos data, corrobora si es que es válida para insertarla en la base de datos
            {
                ModelState.AddModelError("", "La creación del Departamento no se pudo completar porque tuvo un error, intente de nuevo");
                return View(tareaVM);
            }


            var tarea = new Tarea
            {
                Nombre = tareaVM.Nombre,
                Descripcion = tareaVM.Descripcion,
                FotoUrl = tareaVM.FotoUrl,
                IdDepartamento = tareaVM.IdDepartamento,
                FechaCreacion = DateTime.Now,
                FechaEntrega = tareaVM.FechaEntrega
            };

            // Si es válido añadimos
            _tareaR.Add(tarea);

            return RedirectToAction("Detail", "Sede", new { id = tareaVM.IdDepartamento }); // El parámetro que necesita el Método Detail del Controlador de Sede
        }




        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var tarea = await _tareaR.GetByIdAsync(id);
            if (tarea == null) return View("Error");
            var tareaVM = new EditTareaVM
            {
                Id = id,
                Nombre = tarea.Nombre,
                Descripcion = tarea.Descripcion,
                FotoUrl = tarea.FotoUrl,
                IdDepartamento = tarea.IdDepartamento,
                Departamento = tarea.Departamento,
                FechaEntrega = tarea.FechaEntrega,
            };
            return View(tareaVM);
        }



        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTareaVM tareaVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", tareaVM);
            }


            var tarea = new Tarea
            {
                Id = id,
                Nombre = tareaVM.Nombre,
                Descripcion = tareaVM.Descripcion,
                FotoUrl = tareaVM.FotoUrl,
                IdDepartamento = tareaVM.IdDepartamento,
                Departamento = tareaVM.Departamento,
                FechaEntrega = tareaVM.FechaEntrega,
            };

            _tareaR.Update(tarea);

            return RedirectToAction("Detail", "Departamento", new { id = tareaVM.IdDepartamento });

        }



        // [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _tareaR.GetByIdAsync(id);
            if (tarea == null) return View("Error");

            var departamentoId = tarea.IdDepartamento;

            _tareaR.Delete(tarea); // Acá solo se pasa el club y en Entity Framework va a hacer todo el trabajo por nosotros

            return RedirectToAction("Detail", "Departamento", new { id = departamentoId });
        }


    }
}


using System;
using Microsoft.AspNetCore.Mvc;
using Tasq.Interfaces;
using Tasq.Models;
using Tasq.ViewModels;
using static System.Net.WebRequestMethods;

namespace Tasq.Controllers
{
	public class DepartamentoController : Controller
	{
        private readonly ITareaRepository _tareaR; // ya no se nececita eso de _context porque ya eso está hecho con la Interface y los Repositories
        private readonly IDepartamentoRepository _depaR; // ya no se nececita eso de _context porque ya eso está hecho con la Interface y los Repositories
        private readonly ISedeRepository _sedeR; // ya no se nececita eso de _context porque ya eso está hecho con la Interface y los Repositories
        private readonly IHttpContextAccessor _httpCA; // Para regresar lo del AppUserId en el form de Create

        public DepartamentoController(ITareaRepository tareaR, IDepartamentoRepository depaR, ISedeRepository sedeR, IHttpContextAccessor httpCA)
		{
            _tareaR = tareaR;
            _depaR = depaR;
            _sedeR = sedeR;
            _httpCA = httpCA;
        }


        public async Task<IActionResult> Index() 
        {
            IEnumerable<Departamento> dept = await _depaR.GetAll(); 
            return View(dept);
        }


        public async Task<IActionResult> Detail(int id)
        {
            Departamento dept = await _depaR.GetByIdAsync(id);

            IEnumerable<Tarea> tareas = await _tareaR.GetTareasByIdDepartamento(id);

            var vM = new DetailDepartamentoVM
            {
                Departamento = dept,
                Tareas = tareas
            };

            return View(vM);
        }




        // [Authorize(Roles = "Admin")]
        [HttpGet("departamento/create/{idSede}")]
        public async Task<IActionResult> Create(int idSede)
        {
            Sede sede = await _sedeR.GetByIdAsync(idSede);

            if (sede == null) return View("Error");
            

            var datos = new CreateDepartamentoVM
            {
                NombreSede = sede.Nombre,
                IdSede = sede.Id,

            };

            return View(datos);
        }




        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartamentoVM depaVM)
        {
            if (!ModelState.IsValid) // Eso de ModelState es una clase en donde cuando postemaos data, corrobora si es que es válida para insertarla en la base de datos
            {
                ModelState.AddModelError("", "La creación del Departamento no se pudo completar porque tuvo un error, intente de nuevo");
                return View(depaVM);
            }

            var depa = new Departamento
            {
                Nombre = depaVM.Nombre,
                Descripcion = depaVM.Descripcion,
                FotoUrl = depaVM.FotoUrl,
                IdSede = depaVM.IdSede,
            };

            // Si es válido añadimos
            _depaR.Add(depa);

            return RedirectToAction("Detail", "Sede", new { id = depaVM.IdSede }); // El parámetro que necesita el Método Detail del Controlador de Sede
        }





        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var depa = await _depaR.GetByIdAsync(id);
            if (depa == null) return View("Error");
            var depaVM = new EditDepartamentoVM
            {
                Id = id,
                Nombre = depa.Nombre,
                Descripcion = depa.Descripcion,
                FotoUrl = depa.FotoUrl,
                IdSede = depa.IdSede,
                Sede = depa.Sede,
            };
            return View(depaVM);
        }



        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditDepartamentoVM depaVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", depaVM);
            }


            var depa = new Departamento
            {
                Id = id,
                Nombre = depaVM.Nombre,
                Descripcion = depaVM.Descripcion,
                FotoUrl = depaVM.FotoUrl,
                IdSede = (int)depaVM.IdSede,
                Sede = depaVM.Sede,
            };

            _depaR.Update(depa);

            return RedirectToAction("Detail", "Departamento", new { id = depaVM.IdSede });

        }



        // [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteDepartamento(int id)
        {
            var depa = await _depaR.GetByIdAsync(id);
            if (depa == null) return View("Error");

            var sedeId = depa.IdSede;

            _depaR.Delete(depa); // Acá solo se pasa el club y en Entity Framework va a hacer todo el trabajo por nosotros

            return RedirectToAction("Detail", "Sede", new { id = sedeId });
        }


    }
}


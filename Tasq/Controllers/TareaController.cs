using System;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasq.Interfaces;
using Tasq.Models;
using Tasq.ViewModels;

namespace Tasq.Controllers
{
    [Authorize]
    public class TareaController : Controller
	{
        private readonly ITareaRepository _tareaR; 
        private readonly IDepartamentoRepository _depaR;
        private readonly ISedeRepository _sedeR; 


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


        [HttpPost]
        public async Task<IActionResult> Create(CreateTareaVM tareaVM)
        {
            if (!ModelState.IsValid)
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
                FechaEntrega = tareaVM.FechaEntrega,
                Prioridad = 1
            };

            // Si es válido añadimos
            _tareaR.Add(tarea);

            return RedirectToAction("Detail", "Departamento", new { id = tareaVM.IdDepartamento }); 
        }



        [Authorize(Roles = "admin")]
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



        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTareaVM tareaVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error al editar Trarea");
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



        [HttpPost]
        public async Task<IActionResult> Me(int idTarea, string returnUrl)
        {
            var Iduser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (Iduser == null) return RedirectToAction("Login", "Account");

            var tarea = await _tareaR.GetByIdAsync(idTarea);
            if (tarea == null) return View("Error");

            //tarea.IdUser = Iduser;

            // Asignar o desasignar la tarea
            if (tarea.IdUser == Iduser) tarea.IdUser = null;
            else tarea.IdUser = Iduser;

            _tareaR.Update(tarea);

            // Así se saca la url de la página en donde estabamos
            var refererUrl = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(refererUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            else return RedirectToAction("Index", "Tarea");

        }



        [HttpPost]
        public async Task<IActionResult> Status(int idTarea, string returnUrl)
        {
            var tarea = await _tareaR.GetByIdAsync(idTarea);
            if (tarea == null) return View("Error");

            if (tarea.Prioridad != 0)
            {
                tarea.Prioridad = 0;
                tarea.FechaEntrega = DateTime.Now;
            }


            _tareaR.Update(tarea);
            var refererUrl = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(refererUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            else return RedirectToAction("Index", "Tarea");

        }



        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _tareaR.GetByIdAsync(id);
            if (tarea == null) return View("Error");

            var departamentoId = tarea.IdDepartamento;

            _tareaR.Delete(tarea); 

            return RedirectToAction("Detail", "Departamento", new { id = departamentoId });
        }


    }
}


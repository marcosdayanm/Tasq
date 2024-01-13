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

        // Index GET, se mandan todas las tareas para que sean desplegadas en una página
        public async Task<IActionResult> Index()
        {
            IEnumerable<Tarea> tarea = await _tareaR.GetAll();
            return View(tarea);
        }


        // Create GET, se crea una tarea en el departamento el cual se pase su id en el URL
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


        // Create POST, se registran los datos del fromulario y se crea un nuevo objeto de tarea para añadirlo a la DB
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


        // Edit GET, se busca la trea por su Id y se mandan sus daros al front para que puedan ser editados
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


        // Edit POST, se toman los datos del formulario POST mandado desde el front, se revisa que el ModelState sea válido y se actualiza en la DB
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


        // Me POST, función para actualizar la tarea añadiendo o quitando a el responsable de hacerla, ésta función se ejecuta de manera AJAX, con ayuda de JS
        [HttpPost]
        public async Task<IActionResult> Me(int idTarea, string returnUrl)
        {
            var Iduser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (Iduser == null) return RedirectToAction("Login", "Account");

            var tarea = await _tareaR.GetByIdAsync(idTarea);
            if (tarea == null) return View("Error");

            // Asignar o desasignar la tarea
            if (tarea.IdUser == Iduser) tarea.IdUser = null;
            else tarea.IdUser = Iduser;

            _tareaR.Update(tarea);

            // Así se saca la url de la página en donde estabamos
            var refererUrl = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(refererUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            else return RedirectToAction("Index", "Tarea");

        }


        // Status, ésta función sirve para completar las tareas una vez que el responsable las haya completado, el campo de prioridad se actualiza a 0 indicando que ya se completó la tarea para que la tarea se pueda separar a la lista de completadas al desplegarlas en el front
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


        // Delete POST, se bisca la tarea por Id en la DB y se elimina
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


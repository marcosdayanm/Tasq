using System;
using Microsoft.AspNetCore.Mvc;
using Tasq.Interfaces;
using Tasq.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Tasq.ViewModels;

namespace Tasq.Controllers
{
    [Authorize]
    public class SedeController : Controller
	{

        private readonly ISedeRepository _sedeR;
        private readonly IDepartamentoRepository _depaR;
        private readonly ITareaRepository _tareaR;

        public SedeController(ISedeRepository sedeR, IDepartamentoRepository depaR, ITareaRepository tareaR)
		{
            _sedeR = sedeR;
            _depaR = depaR;
            _tareaR = tareaR;
        }


        // Recordar hacer async las funciones porque así hicimos las de los repos, también poner IEnumeable que es una clase que hereda de las listas
        public async Task<IActionResult> Index() // Un IActionResult deja regresar diferentes aciones, casi siempre se regresa una view, es lo más común. Éste es el CONTROLLER
        {
            IEnumerable<Sede> sedes = await _sedeR.GetAll(); // Usando las funciones de Repository 
            return View(sedes); // Estamos mandando el resultado de la consulta al view para poder trabajar con el ahí. Ésta es la VIEW
        }


        public async Task<IActionResult> Detail(int id)
        {
            Sede sede = await _sedeR.GetByIdAsync(id);
            IEnumerable<Departamento> departamentos = await _depaR.GetDepartamentosByIdSede(id);
            IEnumerable<Tarea> tareas = await _tareaR.GetTareasByIdSede(id);

            var vM = new DetailSedeVM
            {
                Sede = sede,
                Departamentos = departamentos,
                Tareas = tareas,
                // Tareas = tareas
            };

            return View(vM);
        }



        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateSedeVM sedeVM)
        {
            if (!ModelState.IsValid) // Eso de ModelState es una clase en donde cuando postemaos data, corrobora si es que es válida para insertarla en la base de datos
            {
                ModelState.AddModelError("", "La creación de la Sede no se pudo completar porque tuvo un error, intente de nuevo");
                return View(sedeVM);
            }

            var sede = new Sede
            {
                Nombre = sedeVM.Nombre,
                Descripcion = sedeVM.Descripcion,
                FotoUrl = sedeVM.FotoUrl,
                Direccion = new Direccion
                {
                    Calle = sedeVM.Direccion.Calle,
                    Ciudad = sedeVM.Direccion.Ciudad,
                    Estado = sedeVM.Direccion.Estado,
                    Pais = sedeVM.Direccion.Pais,
                }

            };

      
            _sedeR.Add(sede);
            return RedirectToAction("Index");
        }




        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var sede = await _sedeR.GetByIdAsync(id);
            if (sede == null) return View("Error");
            var sedeVM = new EditSedeVM
            {
                Id = id,
                Nombre = sede.Nombre,
                Descripcion = sede.Descripcion,
                FotoUrl = sede.FotoUrl,
                IdDireccion = sede.IdDireccion,
                Direccion = sede.Direccion,
            };
            return View(sedeVM);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditSedeVM sedeVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", sedeVM);
            }


            var sede = new Sede
            {
                Id = id,
                Nombre = sedeVM.Nombre,
                Descripcion = sedeVM.Descripcion,
                FotoUrl = sedeVM.FotoUrl,
                IdDireccion = sedeVM.IdDireccion,
                Direccion = sedeVM.Direccion,
            };

            _sedeR.Update(sede);

            return RedirectToAction("Index");

        }


        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSede(int id)
        {
            var sede = await _sedeR.GetByIdAsync(id);
            if (sede == null) return View("Error");

            _sedeR.Delete(sede); // Acá solo se pasa el club y en Entity Framework va a hacer todo el trabajo por nosotros

            return RedirectToAction("Index");
        }



    }
}


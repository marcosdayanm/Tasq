﻿using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasq.Interfaces;
using Tasq.Models;
using Tasq.ViewModels;
using static System.Net.WebRequestMethods;

namespace Tasq.Controllers
{
    [Authorize]
    public class DepartamentoController : Controller
	{
        private readonly ITareaRepository _tareaR; 
        private readonly IDepartamentoRepository _depaR; 
        private readonly ISedeRepository _sedeR; 

        public DepartamentoController(ITareaRepository tareaR, IDepartamentoRepository depaR, ISedeRepository sedeR)
		{
            _tareaR = tareaR;
            _depaR = depaR;
            _sedeR = sedeR;
        }


        // Se cargan todos los departamentos para ser desplegados
        public async Task<IActionResult> Index() 
        {
            IEnumerable<Departamento> dept = await _depaR.GetAll(); 
            return View(dept);
        }


        // Detalles de un departamento, se carga con todas sus tareas y se manda en un viewmodel
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



        // Función pata crear un departamento, se manda el id y el nombre de la sede para que el usuario pueda confirmar que l está crando en la sede correcta
        [HttpGet("departamento/create/{idSede}")]
        [Authorize(Roles = "admin")]
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




        // Crear departamento POST, se toman los datos para crear un objeto de departamento y se ntroducen en una función del repository de los departamentos para crear
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




        // Edit GET, se accede al departamento con el ID pasado y se mandan sus datos a una interfaz para que puedan ser editados
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var depa = await _depaR.GetByIdAsyncNoTracking(id);
            if (depa == null) return View("Error");
            var depaVM = new EditDepartamentoVM
            {
                Id = id,
                Nombre = depa.Nombre,
                Descripcion = depa.Descripcion,
                FotoUrl = depa.FotoUrl,
                IdSede = depa.IdSede,
            };
            return View(depaVM);
        }

        // Edit POST
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditDepartamentoVM depaVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error al editar departamento");
                return View("Edit", depaVM);
            }

            var depa = new Departamento
            {
                Id = id,
                Nombre = depaVM.Nombre,
                Descripcion = depaVM.Descripcion,
                FotoUrl = depaVM.FotoUrl,
                IdSede = depaVM.IdSede,
            };

            _depaR.Update(depa);

            return RedirectToAction("Detail", "Departamento", new { id = depaVM.Id });

        }

        // Delete POST, se regresa a la sede la cual contenía el departamento
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
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


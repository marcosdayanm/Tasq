using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tasq.Data;
using Tasq.Interfaces;
using Tasq.Models;
using Tasq.ViewModels;


namespace Tasq.Controllers
{

    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _uR;
        private readonly ITareaRepository _tareaR;
        private readonly ISedeRepository _sedeR;



        public UserController(IUserRepository iUR, ITareaRepository iTR, ISedeRepository sedeR)
        {
            _uR = iUR;
            _tareaR = iTR;
            _sedeR = sedeR;
        }


        // Index GET, Se cargan los datos de todos los usuarios para desplegarlos en una lista de usuarios
        [HttpGet("users")] // Ésto es para definir que ese es el URL que queremos que se use
        public async Task<IActionResult> Index()
        {
            var users = await _uR.GetAllUsers();
            List<UserVM> result = new List<UserVM>();
            foreach (var user in users)
            {
                var userVM = new UserVM()
                {
                    Id = user.Id,
                    Nombre = user.Nombre,
                    Email = user.Email,
                    FechaNacimiento  = user.FechaNacimiento,
                    FormacionProfesional = user.FormacionProfesional,
                    FotoUrl = user.FotoUrl,
                    IdSede = user.IdSede,
                    Sede = user.Sede,
                };
                result.Add(userVM);
            }


            return View(result);
        }


        // Se cargan los datos del usuario seleccionado para mostrar sus detalles, incluyendo sus tareas oendientes y completadas
        public async Task<IActionResult> Detail(string id)
        {
            var IdUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (IdUser == null) return RedirectToAction("Login", "Account");

            // Se verifica que solo un usuario administrador pueda acceder a los detalles de otros usuarios
            if (IdUser != id)
            {
                if (!User.IsInRole("admin"))
                {
                    return RedirectToAction("Index", "User");
                }
            }

            var user = await _uR.GetUserById(id);
            if (user == null)
            {
                return NotFound(); 
            }

            var tareas = await _tareaR.GetTareasByIdUser(id);

            UserVM userVM = new UserVM()
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Email = user.Email,
                FechaNacimiento = user.FechaNacimiento,
                FormacionProfesional = user.FormacionProfesional != null ? user.FormacionProfesional : null,
                FotoUrl = user.FotoUrl,
                IdSede = user.IdSede,
                Sede = user.Sede,
                Tareas = tareas ?? new List<Tarea>(),
            };

            return View(userVM);
        }




        // Edit GET, se cargan los datos del usuario a editar para desplegarlos en una interfaz
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {

            var IdUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (IdUser == null) return RedirectToAction("Login", "Account");

            // Se verifica que solo un usuario administrador pueda editar otros perfiles de usuarios
            if (IdUser != id)
            {
                if (!User.IsInRole("admin"))
                {
                    return RedirectToAction("Index", "User");
                }
            }


            var user = await _uR.GetUserByIdNoTracking(id);
            IEnumerable<Sede> sedes = await _sedeR.GetAllNoTracking();

            var userVM = new EditUserVM()
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Email = user.Email,
                FechaNacimiento = user.FechaNacimiento,
                FormacionProfesional = user.FormacionProfesional,
                FotoUrl = user.FotoUrl,
                SedeSeleccionadaId = user.IdSede,
                Sedes = sedes,
            };
            return View(userVM);
        }


        // Edit POST, se toman los datos que fueron mandados por el formulario, se verifica que sean válidos y se atualizan en la DB
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserVM userVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error al editar Usuario, intente de nuevo");
                return View("Edit", userVM);
            }


            var user = await _uR.GetUserById(userVM.Id); 


            user.Id = userVM.Id;
            user.Nombre = userVM.Nombre;
            user.Email = userVM.Email;
            user.FechaNacimiento = userVM.FechaNacimiento;
            user.FormacionProfesional = userVM.FormacionProfesional;
            user.FotoUrl = userVM.FotoUrl;
            user.IdSede = userVM.SedeSeleccionadaId;

            Console.WriteLine("El id de la sede es: ");
            Console.WriteLine(user.IdSede);

            _uR.Update(user);

            return RedirectToAction("Detail", new { id = user.Id});

        }



        // Delete POST, se busca al usuario a eliminar en la DB y se elimina, si se eliminó el mismo usuario que está interactuando en la WebApp se hace Log Out, sino, se regresa a la página de los usuarios
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string id)
        {
            // Para checar si no es el mismo id que se pasa, que solo los admin puedan eliminar otros usuarios
            var IdUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (IdUser == null) return RedirectToAction("Login", "Account");

            var user = await _uR.GetUserById(id);
            if (user == null) return View("Error");

            
            if (IdUser != id)
            {
                if (User.IsInRole("admin"))
                {
                    _uR.Delete(user);
                }
                return RedirectToAction("Index", "User");
            }

            _uR.Delete(user); 
            return RedirectToAction("Logout", "Account");
        }



    }
}


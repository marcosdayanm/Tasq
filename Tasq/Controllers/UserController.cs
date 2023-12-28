using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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


        public UserController(IUserRepository iUR, ITareaRepository iTR)
        {
            _uR = iUR;
            _tareaR = iTR;
        }


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
                    IdSede = user.IdSede,
                    Sede = user.Sede,
                };
                result.Add(userVM);
            }


            return View(result);
        }


        public async Task<IActionResult> Detail(string id)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (idUser == null) return RedirectToAction("Login", "Account");

            var user = await _uR.GetUserById(idUser);
            var userVM = new UserVM()
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Email = user.Email,
                FechaNacimiento = user.FechaNacimiento,
                FormacionProfesional = user.FormacionProfesional,
                IdSede = user.IdSede,
                Sede = user.Sede,
                Tareas = _tareaR.GetTareasByIdUser(idUser),
            };

            return View(userVM);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity; // Para el manager
using Microsoft.AspNetCore.Mvc;
using Tasq.Data;
using Tasq.Interfaces;
using Tasq.Models;
using Tasq.ViewModels;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tasq.Controllers
{
    public class AccountController : Controller
    {

        // Los managers nos van a ayudar con muchas funciones de validación
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ISedeRepository _sedeR;
        private readonly IUserRepository _userR;




        public AccountController(UserManager<AppUser> uM, SignInManager<AppUser> siM, ApplicationDbContext c, ISedeRepository sedeR, IUserRepository userR)
        {
            _userManager = uM;
            _signInManager = siM;
            _context = c;
            _sedeR = sedeR;
            _userR = userR;
        }


        // Log In GET
        public IActionResult Login()
        {
            var response = new LoginVM();
            return View(response);
        }


        // Log In POST
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM logVM)
        {
            // Modelstate no válido
            if (!ModelState.IsValid) return View(logVM);


            var user = await _userManager.FindByEmailAsync(logVM.Email);
            if (user != null)
            {
                // Se checa que la contraseña haga match con el user
                var passwordCheck = await _userManager.CheckPasswordAsync(user, logVM.Password);
                if (passwordCheck)
                {
                    // Usuario y contraseña correctos
                    var result = await _signInManager.PasswordSignInAsync(user, logVM.Password, false, false);
                    if (result.Succeeded) return RedirectToAction("Index", "Sede");

                }
                // Contraseña incorrecta
                TempData["Error"] = "Contraseña incorrecta, intente de nuevo";
                return View(logVM);
            }
            // Usuario incorrecto
            TempData["Error"] = "Usuario no encontrado, intente de nuevo";
            return View(logVM);
        }



        // Register GET
        public async Task<IActionResult> Register()
        {

            IEnumerable<Sede> sedes = await _sedeR.GetAll();

            var response = new RegisterVM() 
            {
                Sedes = sedes,
            }; 
            return View(response);
        }


        // Register POST
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM regVM)
        {
            // Modelstate no válido, se mandan los datos insertados de nuevo al front
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Error al crear una cuenta, intente de nuevo";

                IEnumerable<Sede> sedes = await _sedeR.GetAll();
                var response = new RegisterVM()
                {
                    Nombre = regVM.Nombre,
                    Email = regVM.Email,
                    FechaNacimiento = regVM.FechaNacimiento,
                    FormacionProfesional = regVM.FormacionProfesional,
                    FotoUrl = regVM.FotoUrl,
                    SedeSeleccionadaId = regVM.SedeSeleccionadaId,
                    Sedes = sedes,
                    Password = regVM.Password,
                    ConfirmPassword = regVM.ConfirmPassword,
                };

                return View(response);
            }

            // Esta función regresa un objeto de Task<AppUser> entonces podemos validar si se regresó o se regresó null
            var user = await _userManager.FindByEmailAsync(regVM.Email); 
            // Si se encuentra un usuario con el mismo email se lanza un error hy se pide que se de otro email
            if (user != null)
            {
                TempData["Error"] = "La dirección de correo electróncio ya está asociada a una cuenta, intente de nuevo";
                return View(regVM);
            }

            // Se crea neuvo usuario con los datos insertados en el formulario
            var newUser = new AppUser()
            {
                Email = regVM.Email,
                UserName = regVM.Email,
                Nombre = regVM.Nombre,
                FechaNacimiento = regVM.FechaNacimiento,
                FormacionProfesional = regVM.FormacionProfesional,
                FotoUrl = regVM.FotoUrl,
                IdSede = regVM.SedeSeleccionadaId,
            };

            // Se crea el usuario usando IdentityFramework
            var newUserResponse = await _userManager.CreateAsync(newUser, regVM.Password); // Acá ya no se verifica si las passwords matchean porque recordemos que lo pusimos en nuestro ViewModel

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User); // Ésto es poara ponerle el rol de usuario


            // Error en la creación del usuario, se juntan todos los errores y se mandan al front para que el usuario pueda ver cuál fue el error
            if (!newUserResponse.Succeeded)
            {
                var errorMessage = new StringBuilder();

                foreach (var error in newUserResponse.Errors)
                {
                    errorMessage.Append($"{error.Description}; ");

                    // Imprimir los errores en la consola o en el depurador
                    Console.WriteLine($"Error: {error.Code}, {error.Description}");
                    Debug.WriteLine($"Error: {error.Code}, {error.Description}");
                }

                TempData["Error"] = errorMessage.ToString();

                IEnumerable<Sede> sedes = await _sedeR.GetAll();
                var response = new RegisterVM() 
                {
                    Sedes = sedes,
                };

                return View(response);
            }


            // Se hace log in para que una vez creada una cuenta se pueda acceder a la plataforma
            var logVM = new LoginVM()
            {
                Email = regVM.Email,
                Password = regVM.Password,
            };

            var result = await _signInManager.PasswordSignInAsync(newUser, logVM.Password, false, false);

            if (result.Succeeded) return RedirectToAction("Index", "Sede");

            TempData["Error"] = "Error al crear una cuenta, intente de nuevo";
            return View(regVM);


        }


        // Log Out GET
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


    }
}


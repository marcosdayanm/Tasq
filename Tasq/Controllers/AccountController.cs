using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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



        public AccountController(UserManager<AppUser> uM, SignInManager<AppUser> siM, ApplicationDbContext c, ISedeRepository sedeR)
        {
            _userManager = uM;
            _signInManager = siM;
            _context = c;
            _sedeR = sedeR;
        }


        [HttpGet("login")]
        public IActionResult Login()
        {
            var response = new LoginVM(); // para que si por accidente le das refresh a la página no se borren los datos que estabas metiendo de Log In
            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM logVM)
        {
            if (!ModelState.IsValid) return View(logVM);

            var user = await _userManager.FindByEmailAsync(logVM.Email); // Ésto trata de buscar en la DB si ya hay un usuario con éstas características

            if (user != null)
            {
                // User encontrado, validar pw
                var passwordCheck = await _userManager.CheckPasswordAsync(user, logVM.Password);
                if (passwordCheck)
                {
                    // Credenciales correctas, logging in
                    var result = await _signInManager.PasswordSignInAsync(user, logVM.Password, false, false);

                    var claimsPrincipal = User as ClaimsPrincipal; // Esto obtiene el usuario autenticado actual
                    var claimsList = claimsPrincipal.Claims.ToList(); // Aquí tienes todos los claims

                    foreach (var claim in claimsList)
                    {
                        // Inspeccionar cada claim
                        Console.WriteLine(claim.Type + ": " + claim.Value);
                    }


                    if (result.Succeeded) return RedirectToAction("Index", "Sede");

                }

                // pw incorrecta
                TempData["Error"] = "Contraseña incorrecta, intente de nuevo";
                return View(logVM);
            }

            // User no encontrado
            TempData["Error"] = "Usuario no encontrado, intente de nuevo";
            return View(logVM);

        }









        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {

            IEnumerable<Sede> sedes = await _sedeR.GetAll();

            var response = new RegisterVM() // para que si por accidente le das refresh a la página no se borren los datos que estabas metiendo de Log In
            {
                Sedes = sedes,
            }; 
            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM regVM)
        {


            if (!ModelState.IsValid) return RedirectToAction("Register");



            var user = await _userManager.FindByEmailAsync(regVM.Email); // Esta función regresa un objeto de Task<AppUser> entonces podemos validar si se regresó o se regresó null
            // En este caso queremos que no se encuentre porque se está haciendo register
            if (user != null)
            {
                TempData["Error"] = "La dirección de correo electróncio ya está asociada a una cuenta, intente de nuevo";
                return View(regVM);
            }

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

            var newUserResponse = await _userManager.CreateAsync(newUser, regVM.Password); // Acá ya no se verifica si las passwords matchean porque recordemos que lo pusimos en nuestro ViewModel

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User); // Ésto es poara ponerle el rol de usuario


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

                // Asignar el mensaje de error a TempData["Error"]
                TempData["Error"] = errorMessage.ToString();


                IEnumerable<Sede> sedes = await _sedeR.GetAll();
                var response = new RegisterVM() 
                {
                    Sedes = sedes,
                };

                return View(response);
            }


            var logVM = new LoginVM()
            {
                Email = regVM.Email,
                Password = regVM.Password,
            };


            var result = await _signInManager.PasswordSignInAsync(newUser, logVM.Password, false, false);

            if (result.Succeeded) return RedirectToAction("Index", "Sede");
            return RedirectToAction("Register");


        }




        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Account", "Login");
        }


    }
}


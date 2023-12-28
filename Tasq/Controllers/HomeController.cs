using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tasq.Models;
using Tasq.ViewModels;

namespace Tasq.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Index", "Sede");
    }



    //public IActionResult Privacy()
    //{
    //    return View();
    //}



    [Route("Home/Error/{statusCode?}")]
    public IActionResult Error(int? statusCode = null)
    {
        var viewModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            StatusCode = statusCode
        };

        if (statusCode.HasValue)
        {
            if (statusCode == 404)
            {
                viewModel.Message = "Página no encontrada.";
            }
            else if (statusCode == 500)
            {
                viewModel.Message = "Error interno del servidor.";
            }
            // Agrega más casos según sea necesario
        }
        else
        {
            viewModel.Message = "Ocurrió un error inesperado.";
        }

        return View("Error", viewModel);
    }



}


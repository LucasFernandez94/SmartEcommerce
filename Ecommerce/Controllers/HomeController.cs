using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models;
using Newtonsoft.Json;
using UI.Service.DTO;

namespace Ecommerce.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        try
        {
            _logger.LogInformation("Redireccionando a vista index.");
            CustomerDTO customer = null;

            if (TempData["UserData"] != null)
            {
                _logger.LogInformation("Obteniendo datos de usuario.");
                string json = TempData["UserData"].ToString();
                customer = JsonConvert.DeserializeObject<CustomerDTO>(json);
            }

            ViewBag.Message = TempData["Message"];
            return View(customer);
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("Error inesperado al obtener los usurios. con exepción {0}", ex.Message));
            ViewBag.ErrorMessage = "Datos ingresados incorrectos.";
            return View();            
        }        
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

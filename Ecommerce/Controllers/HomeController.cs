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
        CustomerDTO customer = null;

        if (TempData["UserData"] != null)
        {
            string json = TempData["UserData"].ToString();
            customer = JsonConvert.DeserializeObject<CustomerDTO>(json);
        }

        ViewBag.Message = TempData["Message"];
        return View(customer);
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

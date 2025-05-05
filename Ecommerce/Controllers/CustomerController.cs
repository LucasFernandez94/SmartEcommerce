using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UI.Service.DTO;
using UI.Service.Interface;

namespace Ecommerce.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IService<CustomerDTO> _service;
        private ILogger<CustomerController> _log;

        public CustomerController(IService<CustomerDTO> service, ILogger<CustomerController> log)
        {
            _service = service;
            _log = log;
        }
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CustomerModel customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _log.LogInformation("CreateUser: Modelo validado con exito, creando usuario.");
                    CustomerDTO dto = new CustomerDTO();
                    dto.Name = customer.Name;
                    dto.Email = customer.Email;
                    dto.Addres = customer.Addres;
                    dto.Created = DateTime.Now;

                    CustomerDTO result = await _service.CreateAsync(dto);

                    TempData["Message"] = "Usuario creado con éxito";

                    TempData["UserData"] = JsonConvert.SerializeObject(result);
                    _log.LogInformation(string.Format("CreateUser: Usurio creado con exito: {0}", result));
                    return RedirectToAction("Index", "Home");
                }

                return View("CreateCustomer", customer);
            }
            catch (Exception ex)
            {
                _log.LogError(string.Format("Ocurrio un error inesperado al crear el usuario. con le exepción {0}", ex.Message));
                ViewBag.ErrorMessage = "Ocurrio un error inesperado al crear el usuario.";
                return View("CreateCustomer");
            }            
        }
    }
}

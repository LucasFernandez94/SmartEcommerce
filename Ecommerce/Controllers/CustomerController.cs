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

        public CustomerController(IService<CustomerDTO> service)
        {
            _service = service;
        }
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CustomerModel customer)
        {
            if (ModelState.IsValid)
            {
                CustomerDTO dto = new CustomerDTO();
                dto.Name = customer.Name;
                dto.Email = customer.Email;
                dto.Addres = customer.Addres;
                dto.Created = DateTime.Now;

                CustomerDTO result = await _service.CreateAsync(dto);

                TempData["Message"] = "Usuario creado con éxito";

                // Guardar datos en TempData (puede usarse también ViewData o redirigir con ViewModel)
                TempData["UserData"] = JsonConvert.SerializeObject(result);

                return RedirectToAction("Index", "Home");
            }

            // Si algo falla, volvemos a la vista
            return View("CreateCustomer", customer);
        }
    }
}

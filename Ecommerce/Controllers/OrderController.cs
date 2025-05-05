using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using UI.Service.DTO;
using UI.Service.Interface;
using UI.Service.Service.Customer;
using UI.Service.Service.Order;
using UI.Service.Service.Product;

namespace Ecommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly IService<OrderDTO> _service;
        private readonly IService<CustomerDTO> _customerService;
        private readonly IService<ProductDTO> _productService;
        private CustomerService _cusService;
        private ProductService _prodService;
        private ILogger<OrderController> _log;

        public OrderController(IService<OrderDTO> service, IService<CustomerDTO> customerService, CustomerService cusService, IService<ProductDTO> productService, ProductService prodService, ILogger<OrderController> log)
        {
            _service = service;
            _customerService = customerService;
            _cusService = cusService;
            _productService = productService;
            _prodService = prodService;
            _log = log;
        }


        [HttpPost]
        public async Task<IActionResult> GetOrderForUser(int id)
        {
            try
            {
                _log.LogInformation("GetOrderForUser: Obteninedo ordenes historicas.");
                List<OrderDTO> orders = await _service.GetAllAsync();
                if (orders == null)
                {
                    return View(new List<OrderDTO>());
                }

                List<OrderDTO> response = _cusService.AddCustomerById(id, orders);
                _log.LogInformation("Ecommerce.GetOrderForUser: Ordenes obtenidas con exito.");
                return View("GetAllOrders", response);
            }
            catch (Exception ex)
            {
                _log.LogError(string.Format("Ecommerce.GetOrderForUser: Error al realizar la orden. con exepción: {0}", ex.Message));
                ViewBag.Message = "Orden creada correctamente";
                return View(new List<OrderDTO>());
            }
        }


        [HttpPost]
        public async Task<IActionResult> Buy(OrderDTO order)
        {
            try
            {
                if (!ModelState.IsValid) {
                    ViewBag.ErrorMessage = "Datos ingresados incorrectos.";
                    return View("CreateOrder", order);
                }

                var customers = await _customerService.GetAllAsync();
                var matchedCustomer = _cusService.AddCustomer(customers, order.Customer);

                if (matchedCustomer == null) {
                    ViewBag.ErrorMessage = "No se encontro el Usuario.";
                    return View("CreateOrder", order);
                }

                var matchedProducts = _prodService.AddProducts(order.Products);

                if (matchedProducts == null) {
                    ViewBag.ErrorMessage = "No se encontro el Producto en Stock";
                    return View("CreateOrder", order);
                }
                var dbOrder = new OrderDTO
                {
                    OrderDate = DateTime.Now,
                    Customer = matchedCustomer,
                    Products = matchedProducts,
                    PurchaseTotal = order.PurchaseTotal
                };

                var createdOrder = await _service.CreateAsync(dbOrder);

                TempData["SuccessMessage"] = "Orden creada con éxito";
                TempData["CreatedOrder"] = JsonConvert.SerializeObject(createdOrder);

                ViewBag.Message = "Orden creada correctamente";
                _log.LogInformation("Ecommerce.Buy: Compra exitosa");
                return View("CreateOrder", order);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                ViewBag.Message = "Ocurrio un error al realizar la compra";
                return View("CreateOrder");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            try
            {
                var productos = await _productService.GetAllAsync();

                var model = new OrderDTO
                {
                    OrderDate = DateTime.Now,
                    Products = productos,
                    Customer = null
                };
                _log.LogInformation("Ecommerce.CreateOrder: Exito al obtener los productos");
                return View(model);
            }
            catch (Exception ex)
            {
                _log.LogError("Ecommerce.CreateOrder: Error al obtener los productos: " + ex.Message);
                ViewBag.Message = "Orden creada correctamente";
                return View();
            }
        }
    }
}

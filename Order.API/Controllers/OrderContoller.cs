using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Order.Aplication.Models;
using Order.Aplication.Services;
using Order.Domain.Entity;
using System.ComponentModel.DataAnnotations;
using Newtonsoft;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Order.Domain.DTO;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;

        public OrderController(OrderService service, IMapper mapper, ILogger<OrderController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        [Route("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _service.GetAllAsync();
                var orders = await _service.CreateResponse(products);

                _logger.LogInformation(string.Format("OrderContoller: GetAll(): Obtenido con exito con los datos: {0}", JsonConvert.SerializeObject(products, Formatting.None)));
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("OrderContoller: GetAll(): Error inesperado: {0}", ex));
                return NotFound(ex.Message);
            }           
        }


        [HttpGet("GetOrderById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id no puede ser menor o igual a 0.");
                }
                var product = await _service.GetByIdAsync(id);
                if (product == null)
                    return NotFound();

                _logger.LogInformation(string.Format("OrderContoller: GetOrderById(): Obtenido con exito con los datos: {0}", JsonConvert.SerializeObject(product, Formatting.None)));

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("OrderContoller: GetOrderById(): Error inesperado: {0}", ex));
                return NotFound(ex.Message);
            }

        }


        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderModel order)
        {
            try
            {
                OrderModel model = new OrderModel();
                string message = string.Empty;

                if (!order.IsValid(out message))
                {
                    return BadRequest(message);
                }
                model.Products = await _service.GetProducts(order.Products);
                if (model.Products != null && model.Products.Count > 0)
                {
                    model.Customer = await _service.GetCustomer(order.Customer);

                    if (model.Customer != null)
                    {
                        model.PurchaseTotal = _service.CalculateTotal(order.Products);
                        var prod = _service.AddProductEntitie(model);
                        await _service.AddAsync(prod);

                        _logger.LogInformation(string.Format("OrderContoller: CreateOrder(): Datos Guardados con exito: {0}", JsonConvert.SerializeObject(prod, Formatting.None)));

                        return Ok(model);
                    }
                }
                else
                {

                    List<ProductModel> errors = new List<ProductModel>();

                    errors = await _service.GetPrductsError(order.Products);

                    _logger.LogInformation(string.Format("OrderContoller: CreateOrder(): Problemas al obtener los productos : {0}", JsonConvert.SerializeObject(errors, Formatting.None)));

                    return BadRequest(errors);
                }
                return BadRequest(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("OrderContoller: CreateOrder(): Error inesperado: {0}", ex));

                return NotFound(ex.Message);
            }

        }

        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateProduct(Order.Domain.Entity.Order order)
        {
            try
            {
                string message = string.Empty;
                if (!order.IsValid(out message)) {
                    return BadRequest(message);
                }
                Order.Domain.Entity.Order existingProduct = await _service.GetByIdAsync(order.Id);
                if (existingProduct == null)
                    return NotFound("La orden no existe.");

                existingProduct.OrderDate = order.OrderDate;
                existingProduct.Customer = order.Customer;
                existingProduct.Products = order.Products;
                existingProduct.PurchaseTotal = order.PurchaseTotal;


                await _service.UpdateAsync(existingProduct);
                _logger.LogInformation(string.Format("OrderContoller: UpdateOrder(): Datos Actualizados con exito: {0}", JsonConvert.SerializeObject(existingProduct, Formatting.None)));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("OrderContoller: UpdateOrder(): Error inesperado: {0}", ex));

                return NotFound(ex.Message);
            }
        }


        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id <= 0) {
                    return BadRequest("El id no puede ser 0 o menor");
                }

                var existingProduct = await _service.GetByIdAsync(id);
                if (existingProduct == null)
                    return NotFound();

                await _service.DeleteAsync(existingProduct);
                _logger.LogInformation(string.Format("OrderContoller: DeleteOrder(): Datos Eliminados con exito: {0}", JsonConvert.SerializeObject(existingProduct, Formatting.None)));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("OrderContoller: DeleteOrder(): Error inesperado: {0}", ex));
                return NotFound(ex.Message);
            }            
        }
    }
}

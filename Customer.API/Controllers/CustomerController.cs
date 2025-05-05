using AutoMapper;
using Customer.Aplication.Models;
using Customer.Aplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Customer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(CustomerService service, IMapper mapper, ILogger<CustomerController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var customers = await _service.GetAllAsync();
                
                _logger.LogInformation(string.Format("CustomerController: GetAll(): Obtenido con exito con los datos: {0}", JsonConvert.SerializeObject(customers, Formatting.None)));
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("CustomerController: GetAll(): Error inesperado: {0}", ex));
                return NotFound(ex.Message);
            }           
        }


        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0) {
                    return BadRequest("El od no puede ser menor o igual a 0.");
                }
                var customers = await _service.GetByIdAsync(id);
                if (customers == null)
                    return NotFound();
                _logger.LogInformation(string.Format("CustomerController: GetById(): Obtenido con exito con los datos: {0}", JsonConvert.SerializeObject(customers, Formatting.None)));

                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("CustomerController: GetById(): Error inesperado: {0}", ex));
                return NotFound(ex.Message);
            }            
        }


        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] CustomerModel customers)
        {
            try
            {
                string message = string.Empty;
                if (customers.IsValid(out message)) {
                    return BadRequest(message);
                }
                var cus = _service.AddProductEntitie(customers);
                await _service.AddAsync(cus);

                _logger.LogInformation(string.Format("CustomerController: AddProduct(): Obtenido con exito con los datos: {0}", JsonConvert.SerializeObject(cus, Formatting.None)));
                return Ok(cus);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("CustomerController: AddProduct(): Error inesperado: {0}", ex));
                return NotFound(ex.Message);
            }

        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Customer.Domain.Entities.Customer customers)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.ErrorCount);
                }
                Customer.Domain.Entities.Customer existingCustomer = await _service.GetByIdAsync(customers.Id);
                if (existingCustomer == null)
                    return NotFound();

                existingCustomer.Name = customers.Name;
                existingCustomer.Email = customers.Email;
                existingCustomer.Addres = customers.Addres;
                existingCustomer.Created = customers.Created;


                await _service.UpdateAsync(existingCustomer);
                _logger.LogInformation(string.Format("CustomerController: UpdateProduct(): Obtenido con exito con los datos: {0}", JsonConvert.SerializeObject(existingCustomer, Formatting.None)));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("CustomerController: UpdateProduct(): Error inesperado: {0}", ex));
                return NotFound(ex.Message);
            }
        }


        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id <= 0) {
                    return BadRequest("El id no puede ser menor o igual a 0.");
                }
                var existingCustomer = await _service.GetByIdAsync(id);
                if (existingCustomer == null)
                    return NotFound();

                await _service.DeleteAsync(existingCustomer);
                _logger.LogInformation(string.Format("CustomerController: DeleteProduct(): Obtenido con exito con los datos: {0}", JsonConvert.SerializeObject(existingCustomer, Formatting.None)));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("CustomerController: DeleteProduct(): Error inesperado: {0}", ex));
                return NotFound(ex.Message);
            }
        }
    }
}

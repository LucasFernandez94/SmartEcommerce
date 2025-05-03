using API.Aplication.Mapping;
using API.Aplication.Models;
using API.Aplication.Services;
using API.Domain.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ProductService service, IMapper mapper, ILogger<ProductController> logger)
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
                var products = await _service.GetAllAsync();
                _logger.LogInformation(string.Format("ProductController: GetAll(): Obtenido con exito con los datos: {0}", JsonConvert.SerializeObject(products, Formatting.None)));
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("ProductController: GetAll(): Error inesperado: {0}", ex));
                return NotFound(ex.Message);
            }           
        }


        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _service.GetByIdAsync(id);
                if (product == null)
                    return NotFound();

                _logger.LogInformation(string.Format("ProductController: GetById(): Obtenido con exito con los datos: {0}", JsonConvert.SerializeObject(product, Formatting.None)));
                return Ok(product);

            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("ProductController: GetById(): Error inesperado: {0}", ex));
                return NotFound(ex.Message);
            }           
        }


        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductModel product)
        {
            try
            {
                var prod = _service.AddProductEntitie(product);
                await _service.AddAsync(prod);
                _logger.LogInformation(string.Format("ProductController: AddProduct(): Obtenido con exito con los datos: {0}", JsonConvert.SerializeObject(prod, Formatting.None)));
                return Ok(prod);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("ProductController: AddProduct(): Error inesperado: {0}", ex));
                return NotFound(ex.Message);
            }
            
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            try
            {
                Product existingProduct = await _service.GetByIdAsync(product.Id);
                if (existingProduct == null)
                    return NotFound();

                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;

                await _service.UpdateAsync(existingProduct);
                _logger.LogInformation(string.Format("ProductController: AddProduct(): Obtenido con exito con los datos: {0}", JsonConvert.SerializeObject(existingProduct, Formatting.None)));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("ProductController: AddProduct(): Error inesperado: {0}", ex));
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var existingProduct = await _service.GetByIdAsync(id);
                if (existingProduct == null)
                    return NotFound();

                await _service.DeleteAsync(existingProduct);

                _logger.LogInformation(string.Format("ProductController: DeleteProduct(): Obtenido con exito con los datos: {0}", JsonConvert.SerializeObject(existingProduct, Formatting.None)));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("ProductController: AddProduct(): Error inesperado: {0}", ex));
                return BadRequest(ex.Message);
            }           
        }
    }
}

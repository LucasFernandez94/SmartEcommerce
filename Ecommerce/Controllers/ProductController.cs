using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UI.Service.DTO;
using UI.Service.Interface;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IService<ProductDTO> _service;
        private ILogger<ProductController> _log;

        public ProductController(IService<ProductDTO> service, ILogger<ProductController> log)
        {
            _service = service;
            _log = log;
        }

        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                _log.LogInformation(string.Format("GetAllProducts: obteninedo productos."));
                List<ProductDTO> products = await _service.GetAllAsync();
                return View(products);
            }
            catch (Exception ex)
            {
                _log.LogError(string.Format("GetAllProducts: Ocurrio un error inesperado al obtener Productos. con exepción: {0}", ex.Message));
                ViewBag.ErrorMessage = "Error al obtener Productos.";
                return View();
            }
        }
    }
}

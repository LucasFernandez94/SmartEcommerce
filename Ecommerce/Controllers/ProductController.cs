using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UI.Service.DTO;
using UI.Service.Interface;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IService<ProductDTO> _service;

        public ProductController(IService<ProductDTO> service)
        {
            _service = service;
        }

        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                List<ProductDTO> products = await _service.GetAllAsync();
                return View(products);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
    }
}

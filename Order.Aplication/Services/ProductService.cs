using Newtonsoft.Json;
using Order.Domain.DTO;
using Order.Domain.Services;
using System.Text;

namespace Order.Aplication.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7052/Product/");
        }

        public async Task<List<Domain.DTO.ProductDTO>> GetAllProdsAsync()
        {
            var response = await _httpClient.GetAsync($"GetAll");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error al obtener usuario");

            string jsonResp = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Domain.DTO.ProductDTO>>(jsonResp);            
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var response = await _httpClient.GetAsync($"GetById?id={id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al obtener el producto por ID");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductDTO>(jsonResponse);
            return product;
        }

        public async Task<bool> UpdateProducts(ProductDTO product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("UpdateProduct", content); // usa ruta completa si es necesario

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error al actualizar producto");

            return true;
        }

    }
}

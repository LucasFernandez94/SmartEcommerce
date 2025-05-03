using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UI.Service.DTO;
using UI.Service.Interface;

namespace UI.Service.Service.Product
{
    public class ProductService : IService<ProductDTO>
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<ProductDTO> AddProducts(List<ProductDTO> productsLocal)
        {
            List<ProductDTO> list = new List<ProductDTO>();

            foreach (var item in productsLocal)
            {
                if (item.Stock > 0)
                {
                    ProductDTO dbProd = new ProductDTO();
                    dbProd.Id = item.Id;
                    dbProd.Name = item.Name;
                    dbProd.Description = item.Description;
                    dbProd.Price = item.Price;
                    dbProd.Stock = item.Stock;

                    list.Add(dbProd);
                }
            }
            return list;
        }

        public Task<ProductDTO> CreateAsync(ProductDTO entitie)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("GetAll");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al obtener los productos");
            }

            string json = await response.Content.ReadAsStringAsync();

            var productos = JsonConvert.DeserializeObject<List<ProductDTO>>(json);

            return productos ?? new List<ProductDTO>();
        }

        public Task<ProductDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProducts(ProductDTO entitie)
        {
            throw new NotImplementedException();
        }
    }
}

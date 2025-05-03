
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using UI.Service.DTO;
using UI.Service.Interface;

namespace UI.Service.Service.Order
{
    public class OrderService : IService<OrderDTO>
    {
        private readonly HttpClient _httpClient;
        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderDTO> CreateAsync(OrderDTO entity)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("CreateOrder", entity);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new ApplicationException($"Error al crear la orden: {response.StatusCode} - {error}");
                }

                var result = await response.Content.ReadFromJsonAsync<OrderDTO>();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<List<OrderDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("GetAll");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al obtener los productos");
            }

            string json = await response.Content.ReadAsStringAsync();

            var orders = JsonConvert.DeserializeObject<List<OrderDTO>>(json);

            return orders ?? new List<OrderDTO>();
        }

        public Task<OrderDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProducts(OrderDTO entitie)
        {
            throw new NotImplementedException();
        }


    }
}


using Newtonsoft.Json;
using Order.Domain.DTO;
using Order.Domain.Services;

namespace Order.Aplication.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7081/Customer");
        }
        public async Task<CustomerDTO> GetCustomerById(int id)
        {
            var response = await _httpClient.GetAsync($"Customer/GetById?id={id}");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Error al obtener usuario");

            string jsonResp = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CustomerDTO>(jsonResp);
        }
    }
}

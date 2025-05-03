using Newtonsoft.Json;
using System.Text;
using UI.Service.DTO;
using UI.Service.Interface;

namespace UI.Service.Service.Customer
{
    public class CustomerService : IService<CustomerDTO>
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public CustomerDTO AddCustomer(List<CustomerDTO> customers, CustomerDTO customerlocal)
        {
            CustomerDTO dbCustomer = new CustomerDTO();
            foreach (var customer in customers)
            {
                if (customer.Email == customerlocal.Email && customer.Name == customerlocal.Name) {
                    
                    dbCustomer.Id = customer.Id;
                    dbCustomer.Created = customer.Created;
                    dbCustomer.Addres = customer.Addres;
                    dbCustomer.Email = customer.Email;
                    dbCustomer.Name = customer.Name;

                }
            }
            return dbCustomer;
        }

        public List<OrderDTO> AddCustomerById(int id, List<OrderDTO> orders)
        {
            List<OrderDTO> resp = new List<OrderDTO>();
            foreach (var item in orders)
            {
                if (item.Customer.Id == id)
                {
                    resp.Add(item);
                }
            }
            return resp;
        }

        public async Task<CustomerDTO> CreateAsync(CustomerDTO entitie)
        {
            var jsonContent = JsonConvert.SerializeObject(entitie);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("AddProduct", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al crear usuario");
            }

            var jsonResp = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<CustomerDTO>(jsonResp);

            return customer;
        }

        public async Task<List<CustomerDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("GetAll");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al obtener los productos");
            }

            string json = await response.Content.ReadAsStringAsync();

            var customers = JsonConvert.DeserializeObject<List<CustomerDTO>>(json);

            return customers ?? new List<CustomerDTO>();
        }

        public Task<CustomerDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProducts(CustomerDTO entitie)
        {
            throw new NotImplementedException();
        }

    }
}

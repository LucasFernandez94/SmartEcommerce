
using Order.Domain.DTO;

namespace Order.Domain.Services
{
    public interface ICustomerService
    {
        Task<CustomerDTO> GetCustomerById(int id);
    }
}

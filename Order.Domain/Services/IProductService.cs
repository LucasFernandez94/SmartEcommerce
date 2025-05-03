using Order.Domain.DTO;

namespace Order.Domain.Services
{
    public interface IProductService
    {
        Task<ProductDTO> GetById(int id);
        Task<List<ProductDTO>> GetAllProdsAsync();
        Task<bool> UpdateProducts(ProductDTO product);
    }
}

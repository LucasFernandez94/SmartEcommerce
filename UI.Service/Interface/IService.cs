using UI.Service.DTO;

namespace UI.Service.Interface
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task <T> CreateAsync(T entitie);
        Task<bool> UpdateProducts(T entitie);
    }
}

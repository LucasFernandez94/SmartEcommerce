
using API.Domain.Entity;
using API.Domain.Repositories;

namespace API.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable 
    {
        IRepository<T> Repository<T>() where T : class;
        Task<int> Save();
    }
}

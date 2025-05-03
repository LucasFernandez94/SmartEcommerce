using Order.Domain.UnitOfWork;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repository;

namespace Order.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        public UnitOfWork(OrderDbContext context)
        {
            _context = context;
        }
        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
                return (IRepository<T>)_repositories[typeof(T)];

            var repository = new Repository<T>(_context);
            _repositories.Add(typeof(T), repository);

            return repository;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

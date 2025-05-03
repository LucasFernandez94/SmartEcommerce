
using Customer.Domain.Repositories;
using Customer.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastucture.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CustomerDbContext _context;

        public Repository(CustomerDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}

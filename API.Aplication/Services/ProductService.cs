using API.Aplication.Models;
using API.Domain.Entity;
using API.Domain.Repositories;
using API.Domain.Services;
using API.Domain.UnitOfWork;
using API.Domain.Validations;
using API.Infrastucture.Data;
using API.Infrastucture.UoW;
using AutoMapper;

namespace API.Aplication.Services
{
    public class ProductService : ISerivices<Product>
    {
        private readonly AppDbContext _context;
        private UnitOfWork _uow;
        private ProductValidator _validator;
        private readonly IMapper _map;

        public ProductService(AppDbContext context, ProductValidator validator, IMapper mapper)
        {
            _context = context;
            _uow = new UnitOfWork(_context);
            _validator = validator;
            _map = mapper;
        }

        public async Task<bool> AddAsync(Product entity)
        {
            var validator = _validator.Validate(entity);

            if (validator.IsValid)
            {
                await _uow.Repository<Product>().AddAsync(entity);
                await _uow.Save();
                return true;
            }

            return false;
        }

        public async Task DeleteAsync(Product entity)
        {
            await _uow.Repository<Product>().DeleteAsync(entity);
            await _uow.Save();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _uow.Repository<Product>().GetAllAsync();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            return _uow.Repository<Product>().GetByIdAsync(id);
        }

        public async Task UpdateAsync(Product entity)
        {
            await _uow.Repository<Product>().UpdateAsync(entity);
            await _uow.Save();
        }

        public Product AddProductEntitie(ProductModel model)
        {
            return _map.Map<Product>(model);
        }
    }
}
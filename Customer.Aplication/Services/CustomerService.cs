using AutoMapper;
using Customer.Aplication.Models;
using Customer.Domain.Services;
using FluentValidation;
using Customer.Infrastucture.Data;
using Customer.Domain.UnitOfWork;

namespace Customer.Aplication.Services
{
    public class CustomerService : ISerivices<Customer.Domain.Entities.Customer>
    {
        private readonly CustomerDbContext _context;
        private readonly IUnitOfWork _uow;
        private readonly IValidator<Customer.Domain.Entities.Customer> _validator;
        private readonly IMapper _map;

        public CustomerService(CustomerDbContext context, IUnitOfWork uow, IValidator<Customer.Domain.Entities.Customer> validator, IMapper mapper)
        {
            _context = context;
            _uow = uow;
            _validator = validator;
            _map = mapper;
        }

        public async Task<bool> AddAsync(Domain.Entities.Customer entity)
        {
            var validationResult = await _validator.ValidateAsync(entity);

            if (validationResult.IsValid)
            {
                await _uow.Repository<Customer.Domain.Entities.Customer>().AddAsync(entity);
                await _uow.Save();
                return true;
            }

            return false;
        }

        public async Task DeleteAsync(Domain.Entities.Customer entity)
        {
            await _uow.Repository<Customer.Domain.Entities.Customer>().DeleteAsync(entity);
            await _uow.Save();
        }

        public async Task<IEnumerable<Domain.Entities.Customer>> GetAllAsync()
        {
            return await _uow.Repository<Customer.Domain.Entities.Customer>().GetAllAsync();
        }

        public Task<Domain.Entities.Customer> GetByIdAsync(int id)
        {
            return _uow.Repository<Customer.Domain.Entities.Customer>().GetByIdAsync(id);
        }

        public async Task UpdateAsync(Domain.Entities.Customer entity)
        {
            await _uow.Repository<Customer.Domain.Entities.Customer>().UpdateAsync(entity);
            await _uow.Save();
        }

        public Customer.Domain.Entities.Customer AddProductEntitie(CustomerModel model)
        {
            return _map.Map<Customer.Domain.Entities.Customer>(model);
        }
    }
}

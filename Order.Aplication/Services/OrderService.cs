using AutoMapper;
using Order.Aplication.Models;
using Order.Domain.DTO;
using Order.Domain.Entity;
using Order.Domain.Services;
using Order.Domain.Validations;
using Order.Infrastructure.Data;
using Order.Infrastructure.UoW;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Order.Aplication.Services
{
    public class OrderService : ISerivices<Order.Domain.Entity.Order>
    {
        private readonly OrderDbContext _context;
        private UnitOfWork _uow;
        private OrderValidator _validator;
        private readonly IMapper _map;
        private IProductService _prodService;
        private ICustomerService _customerService;
        private readonly IRepository<Order.Domain.Entity.Order> _orderRepository;

        public OrderService(OrderDbContext context, OrderValidator validator, IMapper mapper, IProductService prodService, ICustomerService customerService, IRepository<Order.Domain.Entity.Order> orderRepository)
        {
            _context = context;
            _uow = new UnitOfWork(_context);
            _validator = validator;
            _map = mapper;
            _prodService = prodService;
            _customerService = customerService;
            _orderRepository = orderRepository;
        }

        public async Task<bool> AddAsync(Order.Domain.Entity.Order entity)
        {
            var validator = _validator.Validate(entity);

            if (validator.IsValid)
            {
                await _uow.Repository<Order.Domain.Entity.Order>().AddAsync(entity);
                await _uow.Save();
                return true;
            }

            return false;
        }

        public async Task DeleteAsync(Order.Domain.Entity.Order entity)
        {
            await _uow.Repository<Order.Domain.Entity.Order>().DeleteAsync(entity);
            await _uow.Save();
        }

        public async Task<IEnumerable<Order.Domain.Entity.Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync(o => o.Products);
        }

        public async Task<Order.Domain.Entity.Order> GetByIdAsync(int id)
        {
            return await _uow.Repository<Order.Domain.Entity.Order>()
                     .GetByIdAsync(id, o => o.Products);
        }

        public async Task UpdateAsync(Order.Domain.Entity.Order entity)
        {
            await _uow.Repository<Order.Domain.Entity.Order>().UpdateAsync(entity);
            await _uow.Save();
        }

        public Order.Domain.Entity.Order AddProductEntitie(OrderModel model)
        {
            Order.Domain.Entity.Order order = new Order.Domain.Entity.Order();

            order.OrderDate = model.OrderDate;
            order.Customer = model.Customer.Id;
            order.PurchaseTotal = model.PurchaseTotal;
            foreach (var item in model.Products)
            {
                OrderProduct productDTO = new OrderProduct();
                productDTO.ProductoId = item.Id;

                order.Products.Add(productDTO);
            }

            return order;
        }

        public async Task<List<ProductModel>> GetProducts(List<ProductModel> productos)
        {
            List<ProductModel> producs = new List<ProductModel>();
            List<ProductDTO> respDto = new List<ProductDTO>();
            var prodDTO = await _prodService.GetAllProdsAsync();
            bool isStock = InStock(prodDTO, productos);

            if (isStock)
            {
                respDto = IsContainOrderProducts(prodDTO, productos);
                if (respDto != null) {

                    foreach (var item in respDto)
                    {
                        ProductModel entitie = new ProductModel();
                        bool update = await _prodService.UpdateProducts(item);
                        if (!update)
                        {
                            return producs;
                        }
                        entitie = _map.Map<ProductModel>(item);
                        producs.Add(entitie);
                    }
                }                          
            }

            return producs;
        }

        public async Task<List<ProductModel>> GetPrductsError(List<ProductModel> productos)
        {
            List<ProductModel> response = new List<ProductModel>();
            var prodDTO = await _prodService.GetAllProdsAsync();
            foreach (var prod in productos)
            {
                foreach (var item in prodDTO)
                {
                    if (prod.Id == item.Id)
                    {
                        if (item.Stock == 0) {
                            ProductModel p = new ProductModel();
                            p.Id = item.Id;
                            p.Name = item.Name;
                            p.Description = item.Description;
                            p.Price = item.Price;
                            p.MessageError = "Producto Sin stock.";

                            response.Add(p);
                        }                        
                    }
                }
            }

            return response;
        }

        private bool InStock(List<ProductDTO> prodDTO, List<ProductModel> model)
        {
            foreach (var prod in model)
            {
                foreach (var item in prodDTO)
                {
                    if (prod.Id == item.Id)
                    {
                        if (item.Stock == 0) {
                            return false;
                        }
                    }
                }
            }            
            return true;
        }

        private List<ProductDTO> IsContainOrderProducts(List<ProductDTO> prodDTO, List<ProductModel> model)
        {
            List<ProductDTO> response = new List<ProductDTO>();

            foreach (var prod in model)
            {
                foreach (var item in prodDTO)
                {
                    if (prod.Id == item.Id)
                    {
                        if (item.Stock > prod.Stock)
                        {
                            ProductDTO p = new ProductDTO();

                            p.Id = item.Id;
                            p.Name = item.Name;
                            p.Description = item.Description;
                            p.Price = item.Price;
                            p.Stock = item.Stock - prod.Stock;

                            response.Add(p);
                        }
                    }
                }
            }
            return response;
        }

        public async Task<CustomerModel> GetCustomer(CustomerModel customer)
        {
            CustomerDTO customerDTO = await _customerService.GetCustomerById(customer.Id);
            return _map.Map<CustomerModel>(customerDTO);
        }

        public decimal CalculateTotal(List<ProductModel> productos)
        {
            decimal resp = 0;
            foreach (var item in productos)
            {
                resp += item.Price;
            }
            return resp;
        }

        public async Task<List<OrderProducts>> CreateResponse(IEnumerable<Order.Domain.Entity.Order> order) {
            List<OrderProducts> resp = new List<OrderProducts>();

            foreach (var item in order)
            {
                OrderProducts returnOrder = new OrderProducts();
                returnOrder.Id = item.Id;
                returnOrder.OrderDate = item.OrderDate;
                returnOrder.PurchaseTotal = item.PurchaseTotal;

                returnOrder.Customer = await _customerService.GetCustomerById(item.Customer);
                foreach (var prods in item.Products)
                {
                    ProductDTO products = await _prodService.GetById(prods.ProductoId);
                    returnOrder.Products.Add(products);
                }
                resp.Add(returnOrder);
            }

            return resp;
        }
    }
}

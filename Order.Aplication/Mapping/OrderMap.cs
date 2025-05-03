using AutoMapper;
using Order.Aplication.Models;
using Order.Domain.DTO;

namespace Order.Aplication.Mapping
{
    public class OrderMap : Profile
    {
        public OrderMap()
        {
            CreateMap<OrderModel, Order.Domain.Entity.Order>().ReverseMap();
            CreateMap<Order.Domain.Entity.Order, OrderModel>().ReverseMap();
            CreateMap<ProductModel, ProductDTO>().ReverseMap();
            CreateMap<CustomerModel, CustomerDTO>().ReverseMap();
        }
    }
}

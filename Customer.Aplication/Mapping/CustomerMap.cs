using AutoMapper;
using Customer.Aplication.Models;

namespace Customer.Aplication.Mapping
{
    public class CustomerMap : Profile
    {
        public CustomerMap()
        {
            CreateMap<CustomerModel, Customer.Domain.Entities.Customer>().ReverseMap();
        }
    }
}

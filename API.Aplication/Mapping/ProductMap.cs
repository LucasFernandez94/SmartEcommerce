
using API.Aplication.Models;
using API.Domain.Entity;
using AutoMapper;
namespace API.Aplication.Mapping
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {
            CreateMap<ProductModel, Product>().ReverseMap();
        }
    }
}

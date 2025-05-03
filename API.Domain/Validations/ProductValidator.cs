using API.Domain.Entity;
using FluentValidation;
using System.Data;

namespace API.Domain.Validations
{
    public class ProductValidator : AbstractValidator<Product> {
        public ProductValidator()
        {
            RuleFor(prod => prod.Name).NotEmpty().WithMessage("EL Nombre del producto no puede estra vacío.");
            RuleFor(prod => prod.Price).GreaterThan(0).WithMessage("El precio del producto debe ser mayor a 0");
        }
    }
    //public class ProductValidator
    //{
    //    private readonly Product _product;
    //    public ProductValidator(Product product)
    //    {
    //        _product = product;
    //    }

    //    public bool IsValid()
    //    {
    //        if (string.IsNullOrEmpty(_product.Name))
    //        {
    //            throw new ArgumentException("EL Nombre del producto no puede estra vacío.");
    //        }

    //        if (_product.Price <= 0)
    //        {
    //            throw new ArgumentException("El precio del producto debe ser mayor a 0");
    //        }
    //        return true;
    //    }
    //}
}

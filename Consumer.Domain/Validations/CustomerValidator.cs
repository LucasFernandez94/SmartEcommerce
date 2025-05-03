using FluentValidation;

namespace Customer.Domain.Validations
{
    public class CustomerValidator : AbstractValidator<Customer.Domain.Entities.Customer>
    {
        public CustomerValidator()
        {
            RuleFor(cust => cust.Name).NotEmpty().WithMessage("EL Nombre del usuario no puede estra vacío.");
            RuleFor(cust => cust.Email).NotEmpty().WithMessage("El Email del usuario no puede estra vacío.");
        }
    }
}

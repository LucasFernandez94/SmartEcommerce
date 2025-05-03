using FluentValidation;

namespace Order.Domain.Validations
{
    public class OrderValidator : AbstractValidator<Order.Domain.Entity.Order>
    {
        public OrderValidator()
        {
           
        }
    }
}

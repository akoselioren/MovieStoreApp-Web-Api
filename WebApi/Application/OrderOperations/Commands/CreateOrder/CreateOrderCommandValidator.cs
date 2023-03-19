using FluentValidation;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;

namespace WebApi.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(commend => commend.Model.MovieId).GreaterThan(0);
            RuleFor(commend => commend.Model.CustomerId).GreaterThan(0);
            RuleFor(commend => commend.Model.Price).GreaterThan(0);
        }
    }
}

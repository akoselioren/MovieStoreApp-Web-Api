using FluentValidation;

namespace WebApi.Application.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(commend => commend.Model.MovieId).GreaterThan(0);
            RuleFor(commend => commend.Model.CustomerId).GreaterThan(0);
            RuleFor(commend => commend.Model.Price).GreaterThan(0);
        }
    }
}

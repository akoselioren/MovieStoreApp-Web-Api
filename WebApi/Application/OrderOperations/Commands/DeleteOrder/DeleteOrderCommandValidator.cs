using FluentValidation;

namespace WebApi.Application.OrderOperations.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator() 
        {
            RuleFor(commend => commend.OrderId).GreaterThan(0);
            RuleFor(commend => commend.OrderId).NotEmpty();
        }
    }
}

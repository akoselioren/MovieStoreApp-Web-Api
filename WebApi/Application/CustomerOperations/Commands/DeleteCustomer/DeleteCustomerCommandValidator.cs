using FluentValidation;

namespace WebApi.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(commend => commend.CustomerId).GreaterThan(0);
            RuleFor(commend => commend.CustomerId).NotEmpty();
        }
    }
}

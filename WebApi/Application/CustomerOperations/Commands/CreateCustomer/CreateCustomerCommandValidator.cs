using FluentValidation;

namespace WebApi.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(commend => commend.Model.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(commend => commend.Model.LastName).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Model.Email).NotEmpty().MinimumLength(4).EmailAddress();
            RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(6);
        }
    }
}

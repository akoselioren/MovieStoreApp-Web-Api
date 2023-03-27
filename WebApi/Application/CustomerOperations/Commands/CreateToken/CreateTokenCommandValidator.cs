using FluentValidation;

namespace WebApi.Application.CustomerOperations.Commands.CreateToken
{
    public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
    {
        public CreateTokenCommandValidator()
        {
            RuleFor(command => command.Model.Email).NotEmpty().MinimumLength(4).EmailAddress();
            RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(6);
        }
    }
}

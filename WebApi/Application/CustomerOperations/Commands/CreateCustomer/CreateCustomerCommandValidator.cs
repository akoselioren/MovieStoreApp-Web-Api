using FluentValidation;
using System;

namespace WebApi.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(commend => commend.Model.PayMovieId).GreaterThan(0);
            RuleFor(commend => commend.Model.FavoriteMovieId).GreaterThan(0);
            RuleFor(commend => commend.Model.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(commend => commend.Model.LastName).NotEmpty().MinimumLength(2);
            RuleFor(commend => commend.Model.PhoneNumber).NotEmpty().MinimumLength(10);
        }
    }
}

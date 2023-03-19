using FluentValidation;
using WebApi.Application.ActorOperations.Commands.CreateActor;

namespace WebApi.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(commend => commend.Model.DirectedMovieId).GreaterThan(0);
            RuleFor(commend => commend.Model.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(commend => commend.Model.LastName).NotEmpty().MinimumLength(2);
        }
    }
}

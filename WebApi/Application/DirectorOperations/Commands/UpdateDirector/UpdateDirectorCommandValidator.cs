using FluentValidation;
using WebApi.Application.ActorOperations.Commands.UpdateActor;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateDirectorCommandValidator()
        {
            RuleFor(commend => commend.Model.DirectedMovieId).GreaterThan(0);
            RuleFor(commend => commend.Model.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(commend => commend.Model.LastName).NotEmpty().MinimumLength(2);
        }
    }
}

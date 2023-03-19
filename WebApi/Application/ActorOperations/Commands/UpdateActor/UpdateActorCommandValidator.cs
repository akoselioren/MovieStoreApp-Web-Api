using FluentValidation;

namespace WebApi.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(commend => commend.Model.CastMovieId).GreaterThan(0);
            RuleFor(commend => commend.Model.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(commend => commend.Model.LastName).NotEmpty().MinimumLength(2);
        }
    }
}

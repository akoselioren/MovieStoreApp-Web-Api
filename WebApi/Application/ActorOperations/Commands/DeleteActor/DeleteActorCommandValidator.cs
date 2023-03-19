using FluentValidation;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;

namespace WebApi.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandValidator : AbstractValidator<DeleteActorCommand>
    {
        public DeleteActorCommandValidator()
        {
            RuleFor(commend => commend.ActorId).GreaterThan(0);
            RuleFor(commend => commend.ActorId).NotEmpty();
        }
    }
}

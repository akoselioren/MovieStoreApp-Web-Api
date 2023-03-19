using FluentValidation;
using WebApi.Application.ActorOperations.Commands.DeleteActor;

namespace WebApi.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommandValidator : AbstractValidator<DeleteDirectorCommand>
    {
        public DeleteDirectorCommandValidator()
        {
            RuleFor(commend => commend.DirectorId).GreaterThan(0);
            RuleFor(commend => commend.DirectorId).NotEmpty();
        }
    }
}

using FluentValidation;
namespace WebApi.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator()
        {
            RuleFor(commend => commend.MovieId).GreaterThan(0);
            RuleFor(commend => commend.MovieId).NotEmpty();
        }
    }
}

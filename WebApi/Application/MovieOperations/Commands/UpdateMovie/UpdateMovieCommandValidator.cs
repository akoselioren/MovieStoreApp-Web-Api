using FluentValidation;
using System;

namespace WebApi.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(commend => commend.Model.GenreId).GreaterThan(0);
            RuleFor(commend => commend.Model.Title).NotEmpty().MinimumLength(2);
        }
    }
}

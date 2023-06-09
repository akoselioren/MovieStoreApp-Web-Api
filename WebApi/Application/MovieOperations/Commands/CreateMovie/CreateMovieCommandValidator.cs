﻿using FluentValidation;
using System;

namespace WebApi.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(commend => commend.Model.GenreId).GreaterThan(0);
            RuleFor(commend => commend.Model.DirectorId).GreaterThan(0);
            RuleFor(commend => commend.Model.PublicationDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(commend => commend.Model.Title).NotEmpty().MinimumLength(2);
        }
    }
}

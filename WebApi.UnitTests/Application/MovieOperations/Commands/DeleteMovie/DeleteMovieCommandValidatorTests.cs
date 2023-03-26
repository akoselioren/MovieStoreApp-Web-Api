using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
        {
            // arrange
            DeleteMovieCommand command = new DeleteMovieCommand(null);
            command.MovieId = 0;

            // act
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            var validationResult = validator.Validate(command);

            // // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            DeleteMovieCommand command = new DeleteMovieCommand(null);
            command.MovieId = 1;

            // act
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }

    }
}

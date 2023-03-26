using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.MovieOperations.Commands.UpdateMovie.UpdateMovieCommand;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        //  directorId - genreId - price - id
        // InlineData[(0, 0, 0, 1)] - Valid
        [InlineData(0, 0, 0, 0)]
        [InlineData(0, 0, -1, 1)]
        [InlineData(0, -1, 0, 1)]
        [InlineData(-1, 0, 0, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int directorId, int genreId, int price, int id)
        {
            // arrange
            UpdateMovieCommand command = new UpdateMovieCommand(null);
            command.MovieId = id;
            command.Model = new UpdateMovieModel()
            {
                Title = "A Valid Movie Name",
                DirectorId = directorId,
                GenreId = genreId,
                Price = price
            };

            // act
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGivenReleaseYearIsGreaterThanCurrentYear_Validator_ShouldReturnError()
        {
            // arrange
            UpdateMovieCommand command = new UpdateMovieCommand(null);
            command.Model = new UpdateMovieModel()
            {
                Title = "Lord of The Rings",
                DirectorId = 1,
                GenreId = 1,
                Price = 20,
                PublicationDate = new DateTime(2008, 06, 12)
            };

            // act
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            UpdateMovieCommand command = new UpdateMovieCommand(null);
            command.MovieId = 1;
            command.Model = new UpdateMovieModel()
            {
                Title = "A Valid Movie Name",
                Price = 10,
                GenreId = 1,
                DirectorId = 3,
                PublicationDate = new DateTime(2008, 06, 12)
            };

            // act
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);

        }
    }
}

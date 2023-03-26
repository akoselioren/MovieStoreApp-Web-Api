using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
        {
            // arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 0;

            // act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var validationResult = validator.Validate(command);

            // // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 1;

            // act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}

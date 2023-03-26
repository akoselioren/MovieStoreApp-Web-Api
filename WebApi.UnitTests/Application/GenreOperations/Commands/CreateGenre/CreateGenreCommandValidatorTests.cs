using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        // Name
        // [InlineData("Genre")] - Valid
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("   ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string Name)
        {
            // arrange
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel()
            {
                Name = Name
            };

            // act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel()
            {
                Name = "Yeni Tür"
            };

            // act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}

using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.DirectorOperations.Commands.CreateDirector.CreateDirectorCommand;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        // firstName - lastName
        // [InlineData("Yeni","Director")] - Valid
        [InlineData("Yeni", "")]
        [InlineData("Yeni", " ")]
        [InlineData("Yeni", "  ")]
        [InlineData("Yeni", "   ")]
        [InlineData("", "Director")]
        [InlineData(" ", "Director")]
        [InlineData("   ", "Director")]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("    ", "    ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string firstName, string lastName)
        {
            // arrange
            CreateDirectorCommand command = new CreateDirectorCommand(null, null);
            command.Model = new CreateDirectorModel()
            {
                FirstName = firstName,
                LastName = lastName,
            };

            // act
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            CreateDirectorCommand command = new CreateDirectorCommand(null, null);
            command.Model = new CreateDirectorModel()
            {
                FirstName = "Yeni",
                LastName = "Director",
            };

            // act
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}

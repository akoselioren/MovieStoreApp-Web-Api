using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.ActorOperations.Commands.CreateActor.CreateActorCommand;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        // firstName - lastName
        // [InlineData("New","Actor")] - Valid
        [InlineData("Yeni", "")]
        [InlineData("Yeni", " ")]
        [InlineData("Yeni", "  ")]
        [InlineData("Yeni", "   ")]
        [InlineData("", "Actor")]
        [InlineData(" ", "Actor")]
        [InlineData("   ", "Actor")]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("    ", "    ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string firstName, string lastName)
        {
            // arrange
            CreateActorCommand command = new CreateActorCommand(null, null);
            command.Model = new CreateActorModel()
            {
                FirstName = firstName,
                LastName = lastName,
            };

            // act
            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            CreateActorCommand command = new CreateActorCommand(null, null);
            command.Model = new CreateActorModel()
            {
                FirstName = "Yeni",
                LastName = "Actor",
            };

            // act
            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}

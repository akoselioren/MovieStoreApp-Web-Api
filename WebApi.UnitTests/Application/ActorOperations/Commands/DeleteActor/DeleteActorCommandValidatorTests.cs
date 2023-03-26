using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.ActorOperations.Commands.DeleteActor;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
        {
            // arrange
            DeleteActorCommand command = new DeleteActorCommand(null);
            command.ActorId = 0;

            // act
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            var validationResult = validator.Validate(command);

            // // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            DeleteActorCommand command = new DeleteActorCommand(null);
            command.ActorId = 1;

            // act
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}

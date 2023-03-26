using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.ActorOperations.Commands.UpdateActor.UpdateActorCommand;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            UpdateActorCommand command = new UpdateActorCommand(null);
            command.ActorId = 1;
            command.Model = new UpdateActorModel()
            {
                FirstName = "Updated",
                LastName = "Actor Name"
            };

            // act
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);

        }
    }
}

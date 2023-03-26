using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.DirectorOperations.Commands.UpdateDirector.UpdateDirectorCommand;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            UpdateDirectorCommand command = new UpdateDirectorCommand(null);
            command.DirectorId = 1;
            command.Model = new UpdateDirectorModel()
            {
                FirstName = "Updated",
                LastName = "Director Name"
            };

            // act
            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);

        }
    }
}

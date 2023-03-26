using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
        {
            // arrange
            DeleteDirectorCommand command = new DeleteDirectorCommand(null);
            command.DirectorId = 0;

            // act
            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            var validationResult = validator.Validate(command);

            // // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            DeleteDirectorCommand command = new DeleteDirectorCommand(null);
            command.DirectorId = 1;

            // act
            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}

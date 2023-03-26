using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.OrderOperations.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
        {
            // arrange
            DeleteOrderCommand command = new DeleteOrderCommand(null);
            command.OrderId = 0;

            // act
            DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
            var validationResult = validator.Validate(command);

            // // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            DeleteOrderCommand command = new DeleteOrderCommand(null);
            command.OrderId = 1;

            // act
            DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}

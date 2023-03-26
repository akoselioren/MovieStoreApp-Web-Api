using FluentAssertions;
using System;
using WebApi.Application.OrderOperations.Commands.UpdateOrder;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.OrderOperations.Commands.UpdateOrder.UpdateOrderCommand;

namespace WebApi.UnitTests.Application.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            UpdateOrderCommand command = new UpdateOrderCommand(null);
            command.OrderId = 1;
            command.Model = new UpdateOrderModel()
            {
                CustomerId = 1,
                MovieId = 2,
                OrderDate = new DateTime(2022, 02, 12),
                Price = 65
            };

            // act
            UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);

        }
    }
}

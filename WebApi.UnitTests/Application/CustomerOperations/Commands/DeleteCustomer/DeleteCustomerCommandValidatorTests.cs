using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
        {
            // arrange
            DeleteCustomerCommand command = new DeleteCustomerCommand(null, null);
            command.CustomerId = 3;

            // act
            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            var validationResult = validator.Validate(command);

            // // assert
            validationResult.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            DeleteCustomerCommand command = new DeleteCustomerCommand(null, null);
            command.CustomerId = 2;

            // act
            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}

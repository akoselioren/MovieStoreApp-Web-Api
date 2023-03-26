using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        // email - password - firstName - lastName
        // [InlineData("newcustomer@example.com", "newcustomer", "firstname", "lastname")] - Valid
        [InlineData("customerc@customer.com", "newcustomer", "", "lastname")]
        [InlineData("customerc@customer.com", "newcustomer", " ", "lastname")]
        [InlineData("customerc@customer.com", "newcustomer", "firstname", "")]
        [InlineData("customerc@customer.com", "newcustomer", "firstname", " ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string email, string password, string firstName, string lastName)
        {
            // arrange
            CreateCustomerCommand command = new CreateCustomerCommand(null, null);
            command.Model = new CreateCustomerModel()
            {
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName
            };

            // act
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            CreateCustomerCommand command = new CreateCustomerCommand(null, null);
            command.Model = new CreateCustomerModel()
            {
                Email = "customerc@customer.com",
                Password = "customer123",
                FirstName = "customer",
                LastName = "newcustomer"
            };

            // act
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}

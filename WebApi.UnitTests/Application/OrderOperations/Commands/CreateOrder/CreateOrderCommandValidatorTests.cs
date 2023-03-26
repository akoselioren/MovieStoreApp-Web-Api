using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebApi.Application.MovieOperations.Commands.CreateMovie.CreateMovieCommand;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.UnitTests.TestSetup;
using Xunit;
using FluentAssertions;
using WebApi.Application.OrderOperations.Commands.CreateOrder;

namespace WebApi.UnitTests.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateMovieCommandTestsValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, 0, 50)]
        [InlineData(0, 3, 0)]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 0)]
        [InlineData(0, 1, 95)]
        [InlineData(2, 0, 95)]
        [InlineData(2, 0, 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int CustomerId, int MovieId, int Price)
        {   //Arrange
            CreateOrderCommand command = new CreateOrderCommand(null, null);
            command.Model = new CreateOrderModel()
            {
                CustomerId = CustomerId,
                MovieId = MovieId,
                OrderDate = new DateTime(2001, 06, 12),
                Price = Price
            };
            //Act
            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateOrderCommand command = new CreateOrderCommand(null, null);
            command.Model = new CreateOrderModel()
            {
                CustomerId = 2,
                MovieId = 1,
                OrderDate = DateTime.Now.Date,
                Price=50
            };
            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateOrderCommand command = new CreateOrderCommand(null, null);
            command.Model = new CreateOrderModel()
            {
                CustomerId = 2,
                MovieId = 1,
                OrderDate = DateTime.Now.Date,
                Price = 50
            };
            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

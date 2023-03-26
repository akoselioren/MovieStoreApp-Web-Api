using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.OrderOperations.Commands.UpdateOrder;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.OrderOperations.Commands.UpdateOrder.UpdateOrderCommand;

namespace WebApi.UnitTests.Application.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public UpdateOrderCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenOrderIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // Arrange
            UpdateOrderCommand command = new UpdateOrderCommand(_context);
            command.OrderId = 432;
            command.Model = new UpdateOrderModel() {CustomerId=1,MovieId=2,OrderDate= new DateTime(2022, 02, 12), Price=65};

            // Act & Assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Güncellenecek Order'e ulaşılamadı.");
        }

        [Fact]
        public void WhenGivenOrderNameAlreadyExistsWithDifferentId_Handle_ThrowsInvalidOperationException()
        {
            // Arrange
            UpdateOrderCommand command = new UpdateOrderCommand(_context);
            command.OrderId = 83;
            command.Model = new UpdateOrderModel() { CustomerId = 21, MovieId = 18, OrderDate = new DateTime(2016, 02, 12), Price = 135 };

            // Act & Assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Güncellenecek Order'e ulaşılamadı."); ;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Order_ShouldBeUpdated()
        {
            // arrange
            UpdateOrderCommand command = new UpdateOrderCommand(_context);
            command.OrderId = 3;
            UpdateOrderModel model = new UpdateOrderModel()
            {
                CustomerId = 1,
                MovieId = 1,
                OrderDate = new DateTime(1111, 11, 11),
                Price = 11

            };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            Order order = _context.Orders.SingleOrDefault(order => order.Id == command.OrderId);

            order.Should().NotBeNull();
            order.CustomerId.Should().Be(model.CustomerId);
            order.MovieId.Should().Be(model.MovieId);
            order.OrderDate.Should().Be(model.OrderDate);
            order.Price.Should().Be(model.Price);
        }
    }
}

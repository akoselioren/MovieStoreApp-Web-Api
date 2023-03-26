using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.OrderOperations.Commands.DeleteOrder
{
    public class DeleteOrderCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;

        public DeleteOrderCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }

        [Fact]
        public void WhenGivenOrderIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // arrange
            DeleteOrderCommand command = new DeleteOrderCommand(_dbContext);
            command.OrderId = 876;

            // act & assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Silnecek Sipariş bulunamadı.");
        }

        [Fact]
        public void WhenGivenOrderIsDirectingAMovie_Handle_ThrowsInvalidOperationException()
        {
            // arrange
            DeleteOrderCommand command = new DeleteOrderCommand(_dbContext);
            command.OrderId = 876;

            // act & assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should();
        }

        [Fact]
        public void WhenValidInputsAreGiven_Order_ShouldBeDeleted()
        {
            // arrange
            Order orderWithNoMovies = new Order()
            {
                CustomerId = 19,
                MovieId = 12,
                OrderDate = new DateTime(2008, 06, 12),
                Price = 130
            };
            _dbContext.Orders.Add(orderWithNoMovies);
            _dbContext.SaveChanges();

            Order createdOrder = _dbContext.Orders.SingleOrDefault(order => ((order.CustomerId) == (orderWithNoMovies.CustomerId)));

            DeleteOrderCommand command = new DeleteOrderCommand(_dbContext);
            command.OrderId = createdOrder.Id;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            Order order = _dbContext.Orders.SingleOrDefault(order => order.Id == command.OrderId);

            order.Should().BeNull("Güncellenecek Order'e ulaşılamadı.");
        }
    }
}

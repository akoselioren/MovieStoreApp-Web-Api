using AutoMapper;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.OrderOperations.Queries
{
    public class GetOrderDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetOrderDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenOrderIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // arrange
            GetOrderDetailQuery query = new GetOrderDetailQuery(_context, _mapper);
            query.OrderId = 546;

            // act & assert
            FluentActions
              .Invoking(() => query.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Sipariş bulunamadı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Order_ShouldBeReturned()
        {
            // arrange
            GetOrderDetailQuery query = new GetOrderDetailQuery(_context, _mapper);
            query.OrderId = 2;

            // act
            FluentActions.Invoking(() => query.Handle()).Invoke();

            // assert
            Order order = _context.Orders.SingleOrDefault(order => order.Id == query.OrderId);

            order.Should();
        }
    }
}

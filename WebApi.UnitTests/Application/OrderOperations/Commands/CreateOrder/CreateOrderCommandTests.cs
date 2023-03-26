using AutoMapper;
using Castle.Core.Resource;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderCommandTests : IClassFixture<CommonTestFixture>
    {
         private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateOrderCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Fact]
        public void WhenAlreadyExistingOrderNameIsGiven_Handle_ThrowsInvalidOperationException()
        {
            Order order = new Order()
            {
                CustomerId = 1,
                MovieId = 2,
                OrderDate = new DateTime(2008, 06, 12),
                Price = 60
            };
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            // arrange
            CreateOrderCommand command = new CreateOrderCommand(_dbContext,_mapper);
            command.Model = new CreateOrderModel()
            {
                CustomerId = order.CustomerId,
                MovieId = order.MovieId
            };

            // act & assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Bu sipariş daha önceden oluşturulmuştur.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Order_ShouldBeCreated()
        {
            // arrange
            CreateOrderCommand command = new CreateOrderCommand(_dbContext, _mapper);
            var model = new CreateOrderModel()
            {
                MovieId = 8,
                CustomerId = 11,
                Price = 140,
                OrderDate = new DateTime(2023, 01, 08)
               
            };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var order = _dbContext.Orders.SingleOrDefault(order => order.CustomerId == model.CustomerId);

            order.Should().NotBeNull();
            order.CustomerId.Should().Be(model.CustomerId);
            order.MovieId.Should().Be(model.MovieId);
            order.OrderDate.Should().Be(model.OrderDate);
            order.Price.Should().Be(model.Price);
        }
    }
}

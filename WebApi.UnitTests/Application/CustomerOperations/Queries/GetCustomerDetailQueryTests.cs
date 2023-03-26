using AutoMapper;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.CustomerOperations.Queries
{
    public class GetCustomerDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetCustomerDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenCustomerIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // arrange
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(_context, _mapper);
            query.CustomerId = 333;

            // act & assert
            FluentActions
              .Invoking(() => query.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Müşteri bulunamadı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldBeReturned()
        {
            // arrange
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(_context, _mapper);
            query.CustomerId = 2;

            // act
            FluentActions.Invoking(() => query.Handle()).Invoke();

            // assert
            Customer customer = _context.Customers.SingleOrDefault(customer => customer.Id == query.CustomerId);

            customer.Should().NotBeNull();
        }
    }
}

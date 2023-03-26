using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateCustomerCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Fact]
        public void WhenAlreadyExistingCustomerEmailIsGiven_Handle_ThrowsInvalidOperationException()
        {
            Customer customer = new Customer()
            {
                Email = "abc1@abc.com",
                Password = "abc123",
                FirstName = "abcabc",
                LastName = "cba"

            };
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            // arrange
            CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
            command.Model = new CreateCustomerModel()
            {
                Email = customer.Email,
                Password = customer.Password,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };

            // act & assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Bu müşteri'nin email adresi daha önceden sisteme kayıtlı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldBeCreated()
        {
            // arrange
            CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
            var model = new CreateCustomerModel()
            {
                Email = "cab1@cab.com",
                Password = "cab123",
                FirstName = "bacbac",
                LastName = "cabcab",
            };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var customer = _dbContext.Customers.SingleOrDefault(customer => customer.Email.ToLower() == model.Email.ToLower());

            customer.Should().NotBeNull();
        }
    }
}

using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        public DeleteCustomerCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }

        [Fact]
        public void WhenGivenCustomerIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var claims = new List<Claim>()
      {
          new Claim("customerId", "1")
      };
            mockHttpContextAccessor.Setup(accessor => accessor.HttpContext.User.Claims).Returns(claims);

            DeleteCustomerCommand command = new DeleteCustomerCommand(_dbContext, mockHttpContextAccessor.Object);
            command.CustomerId = 444;

            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Silnecek Müşteri bulunamadı.");
        }

      //  [Fact]
      //  public void WhenGivenCustomerIdIsNotTheSameWithTheCustomerIdInToken_Handle_ThrowsInvalidOperationException()
      //  {
      //      var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
      //      var claims = new List<Claim>()
      //{
      //    new Claim("customerId", "1")
      //};
      //      mockHttpContextAccessor.Setup(accessor => accessor.HttpContext.User.Claims).Returns(claims);

      //      DeleteCustomerCommand command = new DeleteCustomerCommand(_dbContext, mockHttpContextAccessor.Object);
      //      command.CustomerId = 2;

      //      FluentActions
      //        .Invoking(() => command.Handle())
      //        .Should().Throw<InvalidOperationException>()
      //        .And
      //        .Message.Should().Be("Yalnızca kendi hesabınızı silebilirsiniz.");
      //  }

        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldBeDeleted()
        {
            Customer customer = new Customer() { FirstName = "A New", LastName = "Customer To Delete", Email = "customertodelete@example.com", Password = "customertodelete", };
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            Customer newCustomer = _dbContext.Customers.SingleOrDefault(c => c.Email.ToLower() == customer.Email.ToLower());

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var claims = new List<Claim>()
      {
          new Claim("customerId", Convert.ToString(newCustomer.Id))
      };
            mockHttpContextAccessor.Setup(accessor => accessor.HttpContext.User.Claims).Returns(claims);

            DeleteCustomerCommand command = new DeleteCustomerCommand(_dbContext, mockHttpContextAccessor.Object);
            command.CustomerId = newCustomer.Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            Customer deletedCustomer = _dbContext.Customers.SingleOrDefault(c => c.Email.ToLower() == newCustomer.Email.ToLower());

            deletedCustomer.Should().BeNull();
        }
    }
}

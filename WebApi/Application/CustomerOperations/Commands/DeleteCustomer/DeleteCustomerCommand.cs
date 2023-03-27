using System.Linq;
using System;
using WebApi.DbOperations;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using Microsoft.AspNetCore.Http;

namespace WebApi.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand
    {
        public int CustomerId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteCustomerCommand(IMovieStoreDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public void Handle()
        {
            Customer customer = _dbContext.Customers.Include(customer => customer.Orders).SingleOrDefault(customer => customer.Id == CustomerId);
            if (customer is null)
                throw new InvalidOperationException("Silnecek Müşteri bulunamadı.");

            int requestOwnerId = int.Parse(_httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "customerId").Value);
            if (requestOwnerId != customer.Id)
            {
                throw new InvalidOperationException("Yalnızca kendi hesabınızı silebilirsiniz.");
            }

            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();
        }

    }
}

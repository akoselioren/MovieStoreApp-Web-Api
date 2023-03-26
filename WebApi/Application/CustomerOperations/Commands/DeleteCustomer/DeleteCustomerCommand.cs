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
        private readonly IMovieStoreDbContext _dbContext;
        public int CustomerId { get; set; }

        public DeleteCustomerCommand(IMovieStoreDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            Customer customer = _dbContext.Customers.Include(customer => customer.Orders).SingleOrDefault(customer => customer.Id == CustomerId);
            if (customer is null)
                throw new InvalidOperationException("Silnecek Müşteri bulunamadı.");

            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();
        }
    }
}

using System.Linq;
using System;
using WebApi.DbOperations;

namespace WebApi.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand
    {
        private readonly MovieStoreDbContext _dbContext;
        public int CustomerId { get; set; }

        public DeleteCustomerCommand(MovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var customer = _dbContext.Customers.SingleOrDefault(x => x.Id == CustomerId);
            if (customer is null)
                throw new InvalidOperationException("Silnecek Müşteri bulunamadı.");

            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();
        }
    }
}

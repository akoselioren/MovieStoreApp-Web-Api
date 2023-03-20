using System.Linq;
using System;
using WebApi.DbOperations;

namespace WebApi.Application.OrderOperations.Commands.DeleteOrder
{
    public class DeleteOrderCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public int OrderId { get; set; }

        public DeleteOrderCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Handle()
        {
            var order = _dbContext.Orders.SingleOrDefault(x => x.Id == OrderId);
            if (order is null)
                throw new InvalidOperationException("Silnecek Sipariş bulunamadı.");

            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
        }
    }
}

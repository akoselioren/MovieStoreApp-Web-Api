using static WebApi.Application.DirectorOperations.Commands.UpdateDirector.UpdateDirectorCommand;
using WebApi.DbOperations;
using System.Linq;
using System;

namespace WebApi.Application.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommand
    {
        private readonly MovieStoreDbContext _dbContext;
        public int OrderId { get; set; }
        public UpdateOrderModel Model { get; set; }

        public UpdateOrderCommand(MovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var order = _dbContext.Orders.SingleOrDefault(x => x.Id == OrderId);

            if (order is null)
                throw new InvalidOperationException("Güncellenecek Order'e ulaşılamadı.");

            order.MovieId = Model.MovieId != default ? Model.MovieId : order.MovieId;
            order.CustomerId = Model.CustomerId != default ? Model.CustomerId : order.CustomerId;
            order.Price = Model.Price != default ? Model.Price : order.Price;
            order.OrderDate = Model.OrderDate != default ? Model.OrderDate : order.OrderDate;
            _dbContext.SaveChanges();
        }

        public class UpdateOrderModel
        {
            public int MovieId { get; set; }
            public int CustomerId { get; set; }
            public int Price { get; set; }
            public DateTime OrderDate { get; set; }
        }

    }
}

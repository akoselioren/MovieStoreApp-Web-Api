using AutoMapper;
using System.Linq;
using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderCommand
    {
        public CreateOrderModel Model { get; set; }
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateOrderCommand(MovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var order = _dbContext.Orders.SingleOrDefault(x => x.CustomerId == Model.CustomerId && x.MovieId==Model.MovieId);
            if (order is not null)
                throw new InvalidOperationException("Bu sipariş daha önceden oluşturulmuştur.");

            order = _mapper.Map<Order>(Model);

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }
    }

    public class CreateOrderModel
    {
        public int MovieId { get; set; }
        public int CustomerId { get; set; }
        public int Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
}


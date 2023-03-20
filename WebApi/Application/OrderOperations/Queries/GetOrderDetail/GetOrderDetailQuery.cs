using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.OrderOperations.Queries.GetOrderDetail
{
    public class GetOrderDetailQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int OrderId { get; set; }

        public GetOrderDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public OrderDetailViewModel Handle()
        {
            var orders = _dbContext.Orders.Include(x => x.Id).Where(order => order.Id == OrderId).SingleOrDefault();
            if (orders is null)
                throw new InvalidOperationException("Sipariş bulunamadı.");

            OrderDetailViewModel vm = _mapper.Map<OrderDetailViewModel>(orders);
            return vm;
        }
    }

    public class OrderDetailViewModel
    {
        public string Movie { get; set; }
        public string Customer { get; set; }
        public string Price { get; set; }
    }
}


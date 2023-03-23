using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

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
            Order orders = _dbContext.Orders.Where(order => order.Id == OrderId).Include(movie => movie.Movie).Include(customer => customer.Customer).SingleOrDefault();
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
        public DateTime OrderDate { get; set; }
    }
}


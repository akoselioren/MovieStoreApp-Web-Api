using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.Queries.GetOrders
{
    public class GetOrdersQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrdersQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<OrderViewModel> Handle()
        {
            List<Order> orderList = _dbContext.Orders.Where(x => x.Id > 0).Include(movie => movie.Movie).Include(customer => customer.Customer).OrderBy(x => x.Id).ToList<Order>();

            List<OrderViewModel> vm = _mapper.Map<List<OrderViewModel>>(orderList);
            return vm;
        }
    }

    public class OrderViewModel
    {
        public string Movie { get; set; }
        public string Customer { get; set; }
        public string Price { get; set; }

    }
}

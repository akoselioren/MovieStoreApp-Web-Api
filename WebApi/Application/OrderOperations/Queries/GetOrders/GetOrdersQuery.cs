using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;

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
            var movieList = _dbContext.Orders.Include(x => x.MovieId).OrderBy(x => x.MovieId).ToList();

            List<OrderViewModel> vm = _mapper.Map<List<OrderViewModel>>(movieList);
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

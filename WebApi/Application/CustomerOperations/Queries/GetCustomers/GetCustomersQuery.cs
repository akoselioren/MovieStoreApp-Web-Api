using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.OrderOperations.Queries.GetOrders;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomers
{
    public class GetCustomersQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCustomersQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<CustomerViewModel> Handle()
        {
            List<Customer> customerList = _dbContext.Customers.Include(customer => customer.Orders).ThenInclude(order => order.Movie).Include(customer => customer.FavoriteGenres).OrderBy(customer => customer.Id).ToList<Customer>();

            List<CustomerViewModel> vm = _mapper.Map<List<CustomerViewModel>>(customerList);
            return vm;
        }
    }

    public class CustomerViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<OrderViewModel> Orders { get; set; }
        public List<GenresViewModel> FavoriteGenres { get; set; }
    }

}

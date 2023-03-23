using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.OrderOperations.Queries.GetOrders;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int CustomerId { get; set; }
        public GetCustomerDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public CustomerDetailViewModel Handle()
        {
            Customer customer = _dbContext.Customers.Where(customer => customer.Id == CustomerId).Include(customer => customer.FavoriteGenres).Include(customer => customer.Orders).ThenInclude(order => order.Movie).SingleOrDefault();
            if (customer is null)
                throw new InvalidOperationException("Müşteri bulunamadı.");

            CustomerDetailViewModel vm = _mapper.Map<CustomerDetailViewModel>(customer);
            return vm;
        }
    }

    public class CustomerDetailViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<OrderViewModel> Orders { get; set; }
        public List<GenresViewModel> FavoriteGenres { get; set; }
    }
}


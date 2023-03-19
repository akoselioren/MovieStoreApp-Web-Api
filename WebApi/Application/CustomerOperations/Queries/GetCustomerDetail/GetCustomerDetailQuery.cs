using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Application.CustomerOperations.Queries.GetCustomers;
using WebApi.DbOperations;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQuery
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int CustomerId { get; set; }
        public GetCustomerDetailQuery(MovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public CustomerDetailViewModel Handle()
        {
            var movie = _dbContext.Customers.Include(x => x.Order).Where(customer => customer.Id == CustomerId).SingleOrDefault();
            if (movie is null)
                throw new InvalidOperationException("Müşteri bulunamadı.");

            CustomerDetailViewModel vm = _mapper.Map<CustomerDetailViewModel>(movie);
            return vm;
        }
    }

    public class CustomerDetailViewModel
    {
        public string FavoriteMovie { get; set; }
        public string Order { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }

}

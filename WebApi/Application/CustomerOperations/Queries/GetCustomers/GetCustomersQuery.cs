using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomers
{
    public class GetCustomersQuery
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCustomersQuery(MovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<CustomerViewModel> Handle()
        {
            var customerList = _dbContext.Customers.Include(x => x.Movie).OrderBy(x => x.Id).ToList();

            List<CustomerViewModel> vm = _mapper.Map<List<CustomerViewModel>>(customerList);
            return vm;
        }
    }

    public class CustomerViewModel
    {
        public string FavoriteMovie { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }

}

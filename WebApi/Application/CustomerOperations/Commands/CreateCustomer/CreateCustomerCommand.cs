using System.Linq;
using System;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using AutoMapper;

namespace WebApi.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommand
    {
        public CreateCustomerModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateCustomerCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var customer = _dbContext.Customers.SingleOrDefault(x => x.PhoneNumber == Model.PhoneNumber);
            if (customer is not null)
                throw new InvalidOperationException("Bu müşteri'nin telefon numarası daha önceden sisteme kayıtlı.");

            customer = _mapper.Map<Customer>(Model);

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }
    }

    public class CreateCustomerModel
    {
        public int PayMovieId { get; set; }
        public int FavoriteMovieId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}

using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using WebApi.Application.CustomerOperations.Queries.GetCustomers;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerController(IMovieStoreDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            GetCustomersQuery query = new GetCustomersQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(_context, _mapper);
            query.CustomerId = id;

            GetCustomerDetailQueryValidator validator= new GetCustomerDetailQueryValidator();
            validator.ValidateAndThrow(query);
            CustomerDetailViewModel result = query.Handle();

            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateMovie([FromBody] CreateCustomerModel newCustomer)
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);

            command.Model = newCustomer;
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            DeleteCustomerCommand command = new DeleteCustomerCommand(_context, _httpContextAccessor);
            command.CustomerId = id;
            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}

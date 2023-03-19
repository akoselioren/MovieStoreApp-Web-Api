using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using WebApi.Application.CustomerOperations.Queries.GetCustomers;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CustomerController(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            CustomerDetailViewModel result;
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(_context, _mapper);
            query.CustomerId = id;
            result = query.Handle();

            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddMovie([FromBody] CreateCustomerModel newCustomer)
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
            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = id;
            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}

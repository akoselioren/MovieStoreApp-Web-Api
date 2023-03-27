using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.CreateToken;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.Application.CustomerOperations.Commands.RefreshToken;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using WebApi.Application.CustomerOperations.Queries.GetCustomers;
using WebApi.DbOperations;
using WebApi.TokenOperations.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;



namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerController(IMovieStoreDbContext context, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            GetCustomersQuery query = new GetCustomersQuery(_context, _mapper);
            List<CustomerViewModel> result = query.Handle();
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
        public IActionResult CreateCustomer([FromBody] CreateCustomerModel newCustomer)
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);

            command.Model = newCustomer;
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] LoginModel loginInfo)
        {
            CreateTokenCommand command = new CreateTokenCommand( _context, _configuration);
            command.Model = loginInfo;

            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
            validator.ValidateAndThrow(command);

            Token token = command.Handle();

            return token;
        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
            command.RefreshToken = token;
            Token resultAccessToken = command.Handle();
            return resultAccessToken;
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

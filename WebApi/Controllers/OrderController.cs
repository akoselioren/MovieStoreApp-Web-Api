using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using WebApi.Application.OrderOperations.Queries.GetOrders;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public OrderController(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrder()
        {
            GetOrdersQuery query = new GetOrdersQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            OrderDetailViewModel result;
            GetOrderDetailQuery query = new GetOrderDetailQuery(_context, _mapper);
            query.OrderId = id;
            result = query.Handle();

            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddMovie([FromBody] CreateOrderModel newOrder)
        {
            CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);

            command.Model = newOrder;
            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            DeleteOrderCommand command = new DeleteOrderCommand(_context);
            command.OrderId = id;
            DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}

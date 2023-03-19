using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.Application.ActorOperations.Commands.DeleteActor;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.Application.ActorOperations.Queries.GetActors;
using WebApi.DbOperations;
using static WebApi.Application.ActorOperations.Commands.CreateActor.CreateActorCommand;
using static WebApi.Application.ActorOperations.Commands.UpdateActor.UpdateActorCommand;

namespace WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public ActorController(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetActor()
        {
            GetActorsQuery query = new GetActorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ActorDetailViewModel result;
            GetActorsDetailQuery query = new GetActorsDetailQuery(_context, _mapper);
            query.ActorId = id;
            result = query.Handle();

            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddActor([FromBody] CreateActorModel newActor)
        {
            CreateActorCommand command = new CreateActorCommand(_context, _mapper);

            command.Model = newActor;
            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateActor(int id, [FromBody] UpdateActorModel updateActor)
        {
            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = id;
            command.Model = updateActor;
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteActor(int id)
        {
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = id;
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}

﻿using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.Application.DirectorOperations.Queries.GetDirectors;
using WebApi.DbOperations;
using static WebApi.Application.DirectorOperations.Commands.CreateDirector.CreateDirectorCommand;
using static WebApi.Application.DirectorOperations.Commands.UpdateDirector.UpdateDirectorCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class DirectorController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DirectorController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetDirector()
        {
            GetDirectorsQuery query = new GetDirectorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetDirectorsDetailQuery query = new GetDirectorsDetailQuery(_context, _mapper);
            query.DirectorId = id;

            GetDirectorsDetailQueryValidator validator= new GetDirectorsDetailQueryValidator();
            validator.ValidateAndThrow(query);
            DirectorDetailViewModel result = query.Handle();

            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateDirector([FromBody] CreateDirectorModel newDirector)
        {
            CreateDirectorCommand command = new CreateDirectorCommand(_context, _mapper);

            command.Model = newDirector;
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateDirector(int id, [FromBody] UpdateDirectorModel updateDirector)
        {
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = id;
            command.Model = updateDirector;
            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDirector(int id)
        {
            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = id;
            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}

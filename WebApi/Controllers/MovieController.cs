using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using static WebApi.Application.MovieOperations.Commands.CreateMovie.CreateMovieCommand;
using static WebApi.Application.MovieOperations.Commands.UpdateMovie.UpdateMovieCommand;
using FluentValidation.Results;
using FluentValidation;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.Application.MovieOperations.Queries.GetMovies;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;

namespace WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public MovieController(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMovie()
        {
            GetMoviesQuery query = new GetMoviesQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            MovieDetailViewModel result;
            GetMoviesDetailQuery query = new GetMoviesDetailQuery(_context, _mapper);
            query.MovieId = id;
            result = query.Handle();

            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddMovie([FromBody] CreateMovieModel newMovie)
        {
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);

            command.Model = newMovie;
            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieModel updateMovie)
        {
            UpdateMovieCommand command = new UpdateMovieCommand(_context);
            command.MovieId = id;
            command.Model = updateMovie;
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            DeleteMovieCommand command = new DeleteMovieCommand(_context);
            command.MovieId = id;
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}


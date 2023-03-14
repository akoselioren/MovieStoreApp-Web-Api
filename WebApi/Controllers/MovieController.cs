using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.MovieOperations.CreateMovie;
using WebApi.MovieOperations.DeleteMovie;
using WebApi.MovieOperations.GetMovies;
using WebApi.MovieOperations.GetMovieDetail;
using WebApi.MovieOperations.UpdateMovie;
using WebApi.DbOperations;
using static WebApi.MovieOperations.CreateMovie.CreateMovieCommand;
using static WebApi.MovieOperations.UpdateMovie.UpdateMovieCommand;

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
            try
            {
                GetMoviesDetailQuery query = new GetMoviesDetailQuery(_context, _mapper);
                query.MovieId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddMovie([FromBody] CreateMovieModel newMovie)
        {
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            try
            {
                command.Model = newMovie;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieModel updateMovie)
        {
            try
            {
                UpdateMovieCommand command = new UpdateMovieCommand(_context);
                command.MovieId = id;
                command.Model = updateMovie;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
                DeleteMovieCommand command = new DeleteMovieCommand(_context);
                command.MovieId = id;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
            return Ok();
        }
    }
}


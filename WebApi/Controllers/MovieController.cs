using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieStoreDbContext _context;

        public MovieController(MovieStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Movie> GetMovie()
        {
            var movieList = _context.Movies.OrderBy(x => x.Id).ToList<Movie>();
            return movieList;
        }

        [HttpGet("{id}")]
        public Movie GetById(int id)
        {
            var movie = _context.Movies.Where(movie => movie.Id == id).SingleOrDefault();
            return movie;
        }
        [HttpPost]
        public IActionResult AddMovie([FromBody] Movie newMovie)
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Title == newMovie.Title);
            if (movie is not null)
                return BadRequest();

            _context.Movies.Add(newMovie);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] Movie updateMovie)
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (movie is null)
                return BadRequest();

            movie.GenreId = updateMovie.GenreId != default ? updateMovie.GenreId : movie.GenreId;
            movie.RunningTime = updateMovie.RunningTime != default ? updateMovie.RunningTime : movie.RunningTime;
            movie.PublicationDate = updateMovie.PublicationDate != default ? updateMovie.PublicationDate : movie.PublicationDate;
            movie.Title = updateMovie.Title != default ? updateMovie.Title : movie.Title;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (movie is null)
                return BadRequest();

            _context.Movies.Remove(movie);
            return Ok();
        }
    }
}


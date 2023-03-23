using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Application.MovieOperations.Queries.Shared;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Queries.GetMovies
{
    public class GetMoviesQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetMoviesQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<MovieViewModel> Handle()
        {
            List<Movie> movieList = _dbContext.Movies.Where(movie => movie.IsActive).Include(movie => movie.Director).Include(movie => movie.MovieActors).Include(movie => movie.Genre).OrderBy(movie => movie.Id).ToList<Movie>();

            List<MovieViewModel> vm = _mapper.Map<List<MovieViewModel>>(movieList);
            return vm;
        }
    }

    public class MovieViewModel
    {
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public int Price { get; set; }
        public List<ActorsViewModel> MovieActors { get; set; }
    }

   
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Application.MovieOperations.Queries.Shared;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMoviesDetailQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int MovieId { get; set; }
        public GetMoviesDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public MovieDetailViewModel Handle()
        {
            Movie movieList = _dbContext.Movies.Where(movie => movie.Id == MovieId && movie.IsActive).Include(movie => movie.Genre).Include(movie => movie.Director).Include(movie => movie.MovieActors).SingleOrDefault();
                if (movieList is null)
                throw new InvalidOperationException("Film bulunamadı.");


            MovieDetailViewModel vm = _mapper.Map<MovieDetailViewModel>(movieList);
            return vm;
        }
    }

    public class MovieDetailViewModel
    {
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public int Price { get; set; }
        public List<ActorsViewModel> MovieActors { get; set; }
    }
}

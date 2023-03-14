using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.MovieOperations.GetMovies;
using WebApi.DbOperations;
using AutoMapper;

namespace WebApi.MovieOperations.CreateMovie
{
    public class CreateMovieCommand
    {
        public CreateMovieModel Model {get; set; }
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateMovieCommand(MovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var movie = _dbContext.Movies.SingleOrDefault(x => x.Title == Model.Title);

            if (movie is not null)
                throw new InvalidOperationException("Film zaten mevcut");

            movie=_mapper.Map<Movie>(Model);

            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
        }

        public class CreateMovieModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public string RunningTime { get; set; }
            public DateTime PublicationDate { get; set; }
        }
    }
}

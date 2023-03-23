using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using AutoMapper;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommand
    {
        public CreateMovieModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateMovieCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var movie = _dbContext.Movies.SingleOrDefault(x => x.Title == Model.Title);

            if (movie is not null)
                throw new InvalidOperationException("Film zaten mevcut");

            movie = _mapper.Map<Movie>(Model);

            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
        }

        public class CreateMovieModel
        {
            private string title;
            public string Title
            {
                get { return title; }
                set { title = value.Trim(); }
            }
            public int GenreId { get; set; }
            public int DirectorId { get; set; }
            public DateTime PublicationDate { get; set; }
            public int Price { get; set; }
        }
    }
}

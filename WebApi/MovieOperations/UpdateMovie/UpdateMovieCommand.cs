using System.Linq;
using System;
using WebApi.DbOperations;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace WebApi.MovieOperations.UpdateMovie
{
    public class UpdateMovieCommand
    {
        private readonly MovieStoreDbContext _dbContext;
        public int MovieId {get; set;}
        public UpdateMovieModel Model { get; set;}
        public UpdateMovieCommand(MovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var movie = _dbContext.Movies.SingleOrDefault(x => x.Id == MovieId);

            if (movie is null)
                throw new InvalidOperationException("Güncellenecek Film'e ulaşılamadı.");

            movie.Title = Model.Title != default ? Model.Title : movie.Title;
            movie.GenreId = Model.GenreId != default ? Model.GenreId : movie.GenreId;
            _dbContext.SaveChanges();
        }

        public class UpdateMovieModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            //public string RunningTime { get; set; }
            //public DateTime PublicationDate { get; set; }
        }
    }
}

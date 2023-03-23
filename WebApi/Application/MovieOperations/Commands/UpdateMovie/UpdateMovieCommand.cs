using System.Linq;
using System;
using WebApi.DbOperations;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace WebApi.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public int MovieId { get; set; }
        public UpdateMovieModel Model { get; set; }
        public UpdateMovieCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var movie = _dbContext.Movies.SingleOrDefault(x => x.Id == MovieId);

            if (movie is null)
                throw new InvalidOperationException("Güncellenecek Film'e ulaşılamadı.");

            movie.GenreId = Model.GenreId != default ? Model.GenreId : movie.GenreId;
            movie.DirectorId = Model.DirectorId != default ? Model.DirectorId : movie.DirectorId;
            movie.PublicationDate = Model.PublicationDate != default ? Model.PublicationDate : movie.PublicationDate;
            movie.Title = string.IsNullOrEmpty(Model.Title) ? movie.Title : Model.Title.Trim();
            movie.Price = Model.Price != default ? Model.Price : movie.Price;

            if (_dbContext.Movies.Any(m => m.Title.ToLower() == movie.Title.ToLower() && m.Id != MovieId))
                throw new InvalidOperationException("Bu isimde bir film zaten var.");

            _dbContext.SaveChanges();
        }

        public class UpdateMovieModel
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

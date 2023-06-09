﻿using System.Linq;
using System;
using WebApi.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public int MovieId { get; set; }
        public DeleteMovieCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var movie = _dbContext.Movies.SingleOrDefault(x => x.Id == MovieId);
            if (movie is null)
                throw new InvalidOperationException("Silnecek Film bulunamadı.");

            movie.IsActive= false;
            _dbContext.SaveChanges();
        }

    }
}

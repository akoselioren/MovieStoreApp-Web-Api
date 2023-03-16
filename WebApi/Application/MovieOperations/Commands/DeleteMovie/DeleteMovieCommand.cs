using System.Linq;
using System;
using WebApi.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommand
    {
        private readonly MovieStoreDbContext _dbContext;
        public int MovieId { get; set; }
        public DeleteMovieCommand(MovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var movie = _dbContext.Movies.SingleOrDefault(x => x.Id == MovieId);
            if (movie is null)
                throw new InvalidOperationException("Silnecek Film bulunamadı.");

            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();
        }

    }
}

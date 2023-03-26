using System.Linq;
using System;
using WebApi.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public int DirectorId { get; set; }

        public DeleteDirectorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var director = _dbContext.Directors.SingleOrDefault(x => x.Id == DirectorId);
            if (director is null)
                throw new InvalidOperationException("Silnecek Yönetici bulunamadı.");

            bool isDirectingAnyMovie = _dbContext.Movies.Include(movie => movie.Director).Any(movie => movie.IsActive && movie.Director.Id == director.Id);

            if (isDirectingAnyMovie)
                throw new InvalidOperationException("Bu yönetmen bir film yönetmektedir, şu anda silinemez.");

            _dbContext.Directors.Remove(director);
            _dbContext.SaveChanges();
        }

    }
}

using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
         public CreateGenreModel Model { get; set; }
        private readonly MovieStoreDbContext _dbContext;

        public CreateGenreCommand(MovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x=>x.Name==Model.Name);
            if (genre is not null)
                throw new InvalidOperationException("Film türü zaten mevcut.");

            genre = new Genre();
            genre.Name = Model.Name;
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
        }


    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}

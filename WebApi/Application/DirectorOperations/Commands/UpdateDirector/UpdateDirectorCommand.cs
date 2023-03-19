using System.Linq;
using System;
using WebApi.DbOperations;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        private readonly MovieStoreDbContext _dbContext;
        public int DirectorId { get; set; }
        public UpdateDirectorModel Model { get; set; }

        public UpdateDirectorCommand(MovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var director = _dbContext.Directors.SingleOrDefault(x => x.Id == DirectorId);

            if (director is null)
                throw new InvalidOperationException("Güncellenecek Director'e ulaşılamadı.");

            director.FirstName = Model.FirstName != default ? Model.FirstName : director.FirstName;
            director.LastName = Model.LastName != default ? Model.LastName : director.LastName;
            director.DirectedMovieId = Model.DirectedMovieId != default ? Model.DirectedMovieId : director.DirectedMovieId;
            _dbContext.SaveChanges();
        }

        public class UpdateDirectorModel
        {
            public int DirectedMovieId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

    }
}

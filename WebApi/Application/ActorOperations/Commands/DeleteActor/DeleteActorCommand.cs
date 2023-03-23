using System.Linq;
using System;
using WebApi.DbOperations;

namespace WebApi.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public int ActorId { get; set; }

        public DeleteActorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var actor = _dbContext.Actors.SingleOrDefault(x => x.Id == ActorId);
            if (actor is null)
                throw new InvalidOperationException("Silnecek Actor bulunamadı.");



            _dbContext.Actors.Remove(actor);
            _dbContext.SaveChanges();
        }

    }
}

using System.Linq;
using System;
using WebApi.DbOperations;

namespace WebApi.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public int ActorId { get; set; }
        public UpdateActorModel Model { get; set; }

        public UpdateActorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var actor = _dbContext.Actors.SingleOrDefault(x => x.Id == ActorId);

            if (actor is null)
                throw new InvalidOperationException("Güncellenecek Actor'e ulaşılamadı.");

            actor.FirstName = Model.FirstName != default ? Model.FirstName : actor.FirstName;
            actor.LastName = Model.LastName != default ? Model.LastName : actor.LastName;
            actor.CastMovieId = Model.CastMovieId != default ? Model.CastMovieId : actor.CastMovieId;
            _dbContext.SaveChanges();
        }

        public class UpdateActorModel
        {
            public int CastMovieId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}

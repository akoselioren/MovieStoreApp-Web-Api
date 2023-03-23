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

            actor.FirstName = string.IsNullOrEmpty(Model.FirstName) ? actor.FirstName : Model.FirstName.Trim();
            actor.LastName = string.IsNullOrEmpty(Model.LastName) ? actor.LastName : Model.LastName.Trim();


            _dbContext.SaveChanges();
        }

        public class UpdateActorModel
        {
            private string firstName;
            public string FirstName
            {
                get { return firstName; }
                set { firstName = value.Trim(); }
            }
            private string lastName;
            public string LastName
            {
                get { return lastName; }
                set { lastName = value.Trim(); }
            }
        }
    }
}

using AutoMapper;
using static WebApi.Application.MovieOperations.Commands.CreateMovie.CreateMovieCommand;
using WebApi.DbOperations;
using System.Linq;
using System;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommand
    {
        public CreateActorModel Model { get; set; }
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateActorCommand(MovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var actor = _dbContext.Actors.SingleOrDefault(x => x.FirstName == Model.FirstName && x.LastName==Model.LastName);

            if (actor is not null)
                throw new InvalidOperationException("Eklemek istediğiniz Actor zaten mevcut");

            actor = _mapper.Map<Actor>(Model);

            _dbContext.Actors.Add(actor);
            _dbContext.SaveChanges();
        }

        public class CreateActorModel
        {
            public int CastMovieId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}

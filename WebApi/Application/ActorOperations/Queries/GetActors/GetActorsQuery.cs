using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.Application.ActorOperations.Queries.Shared;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Queries.GetActors
{
    public class GetActorsQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetActorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<ActorViewModel> Handle()
        {
            List<Actor> actorList = _dbContext.Actors
      .Include(actor => actor.ActorMovies.Where(movie => movie.IsActive))
        .ThenInclude(movie => movie.Director)
      .Include(actor => actor.ActorMovies.Where(movie => movie.IsActive))
        .ThenInclude(movie => movie.Genre)
      .OrderBy(actor => actor.Id)
      .ToList<Actor>();

            List<ActorViewModel> ActorVm = _mapper.Map<List<ActorViewModel>>(actorList);
            return ActorVm;
        }
    }

    public class ActorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<ActedInMovieViewModel> ActorMovies { get; set; }

    }

}

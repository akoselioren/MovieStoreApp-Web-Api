using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;

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
            var actorList = _dbContext.Actors.Include(x => x.Movie).OrderBy(x => x.Id).ToList();

            List<ActorViewModel> vm = _mapper.Map<List<ActorViewModel>>(actorList);
            return vm;
        }
    }

    public class ActorViewModel
    {
        public string CastMovie { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

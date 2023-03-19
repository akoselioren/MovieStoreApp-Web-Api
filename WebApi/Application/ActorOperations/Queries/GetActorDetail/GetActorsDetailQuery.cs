using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorsDetailQuery
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int ActorId { get; set; }

        public GetActorsDetailQuery(MovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ActorDetailViewModel Handle()
        {
            var actor = _dbContext.Actors.Include(x => x.Movie).Where(actor => actor.Id == ActorId).SingleOrDefault();
            if (actor is null)
                throw new InvalidOperationException("Actor bulunamadı.");


            ActorDetailViewModel vm = _mapper.Map<ActorDetailViewModel>(actor);
            return vm;
        }
    }

    public class ActorDetailViewModel
    {
        public string CastMovie{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Application.ActorOperations.Queries.Shared;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int ActorId { get; set; }

        public GetActorDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ActorDetailViewModel Handle()
        {
            Actor actor = _dbContext.Actors.Where(actor => actor.Id == ActorId)
     .Include(actor => actor.ActorMovies.Where(movie => movie.IsActive))
       .ThenInclude(movie => movie.Director)
     .Include(actor => actor.ActorMovies.Where(movie => movie.IsActive))
       .ThenInclude(movie => movie.Genre)
     .SingleOrDefault();
            if (actor is null)
                throw new InvalidOperationException("Oyuncu bulunamadı.");


            ActorDetailViewModel vm = _mapper.Map<ActorDetailViewModel>(actor);
            return vm;
        }
    }

    public class ActorDetailViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<ActedInMovieViewModel> ActorMovies { get; set; }
    }

   
}

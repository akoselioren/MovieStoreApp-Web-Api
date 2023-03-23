using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.Application.DirectorOperations.Queries.Shared;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectors
{
    public class GetDirectorsQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDirectorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<DirectorViewModel> Handle()
        {
            List<Director> directorList = _dbContext.Directors.Include(director => director.DirectedMovies.Where(movie => movie.IsActive)).ThenInclude(movie => movie.Genre).OrderBy(director => director.Id).ToList<Director>();


            List<DirectorViewModel> vm = _mapper.Map<List<DirectorViewModel>>(directorList);
            return vm;
        }
    }

    public class DirectorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<DirectedMovieViewModel> DirectedMovies { get; set; }
    }
    
}

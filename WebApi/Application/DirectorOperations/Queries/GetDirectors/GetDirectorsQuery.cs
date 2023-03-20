using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;

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
            var directorList = _dbContext.Directors.Include(x => x.Movie).OrderBy(x => x.DirectedMovieId).ToList();

            List<DirectorViewModel> vm = _mapper.Map<List<DirectorViewModel>>(directorList);
            return vm;
        }
    }

    public class DirectorViewModel
    {
        public string DirectedMovie { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorsDetailQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int DirectorId { get; set; }

        public GetDirectorsDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public DirectorDetailViewModel Handle()
        {
            var director = _dbContext.Directors.Include(x => x.Movie).Where(director => director.Id == DirectorId).SingleOrDefault();
            if (director is null)
                throw new InvalidOperationException("Director bulunamadı.");


            DirectorDetailViewModel vm = _mapper.Map<DirectorDetailViewModel>(director);
            return vm;
        }
    }

    public class DirectorDetailViewModel
    {
        public string DirectedMovie { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

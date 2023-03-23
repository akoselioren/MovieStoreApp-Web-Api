using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Application.DirectorOperations.Queries.Shared;
using WebApi.DbOperations;
using WebApi.Entities;

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
            Director director = _dbContext.Directors.Where(director => director.Id == DirectorId)
      .Include(director => director.DirectedMovies.Where(movie => movie.IsActive))
        .ThenInclude(movie => movie.Director)
      .Include(director => director.DirectedMovies.Where(movie => movie.IsActive))
        .ThenInclude(movie => movie.Genre)
      .SingleOrDefault();
            if (director is null)
                throw new InvalidOperationException("Director bulunamadı.");


            DirectorDetailViewModel vm = _mapper.Map<DirectorDetailViewModel>(director);
            return vm;
        }
    }

    public class DirectorDetailViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<DirectedMovieViewModel> DirectedMovies { get; set; }
    }
}

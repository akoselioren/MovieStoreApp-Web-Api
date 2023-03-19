using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Queries.GetMovies
{
    public class GetMoviesQuery
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetMoviesQuery(MovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<MovieViewModel> Handle()
        {
            var movieList = _dbContext.Movies.Include(x=>x.Genre).OrderBy(x => x.Id).ToList();

            List<MovieViewModel> vm = _mapper.Map<List<MovieViewModel>>(movieList);
            return vm;
        }
    }

    public class MovieViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string RunningTime { get; set; }
        public string PublicationDate { get; set; }
    }
}

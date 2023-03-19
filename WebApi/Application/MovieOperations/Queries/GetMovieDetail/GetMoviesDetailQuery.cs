using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMoviesDetailQuery
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int MovieId { get; set; }
        public GetMoviesDetailQuery(MovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public MovieDetailViewModel Handle()
        {
            var movie = _dbContext.Movies.Include(x=>x.Genre).Where(movie => movie.Id == MovieId).SingleOrDefault();
            if (movie is null)
                throw new InvalidOperationException("Film bulunamadı.");


            MovieDetailViewModel vm = _mapper.Map<MovieDetailViewModel>(movie);
            return vm;
        }
    }

    public class MovieDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string RunningTime { get; set; }
        public string PublicationDate { get; set; }
    }
}

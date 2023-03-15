using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using WebApi.MovieOperations.GetMovieDetail;
using WebApi.MovieOperations.GetMovies;
using static WebApi.MovieOperations.CreateMovie.CreateMovieCommand;
using static WebApi.MovieOperations.UpdateMovie.UpdateMovieCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile( )
        {
            CreateMap<CreateMovieModel, Movie>();
            CreateMap<Movie, MovieDetailViewModel>().ForMember(dest =>dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
            CreateMap<Movie,MovieViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));

        }
    }
}

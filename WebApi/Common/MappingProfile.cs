using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using WebApi.Application.GenreOperations.Queryies.GetGenreDetail;
using WebApi.Application.GenreOperations.Queryies.GetGenres;
using WebApi.Application.MovieOperations.Queryies.GetMovieDetail;
using WebApi.Application.MovieOperations.Queryies.GetMovies;
using WebApi.Entities;
using static WebApi.Application.MovieOperations.Commands.CreateMovie.CreateMovieCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile( )
        {
            CreateMap<CreateMovieModel, Movie>();
            CreateMap<Movie, MovieDetailViewModel>().ForMember(dest =>dest.Genre, opt=>opt.MapFrom(src=>src.Genre.Name));
            CreateMap<Movie,MovieViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GetGenreDetailViewModel>();

        }
    }
}

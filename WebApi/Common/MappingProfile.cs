using AutoMapper;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.Application.ActorOperations.Queries.GetActors;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using WebApi.Application.CustomerOperations.Queries.GetCustomers;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.Application.DirectorOperations.Queries.GetDirectors;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.Application.MovieOperations.Queries.GetMovies;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using WebApi.Entities;
using static WebApi.Application.ActorOperations.Commands.CreateActor.CreateActorCommand;
using static WebApi.Application.DirectorOperations.Commands.CreateDirector.CreateDirectorCommand;
using static WebApi.Application.MovieOperations.Commands.CreateMovie.CreateMovieCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile( )
        {
            //Movie
            CreateMap<CreateMovieModel, Movie>();
            CreateMap<Movie, MovieDetailViewModel>().ForMember(dest =>dest.Genre, opt=>opt.MapFrom(src=>src.Genre.Name));
            CreateMap<Movie,MovieViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

            //Genre
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GetGenreDetailViewModel>();

            //Customer
            CreateMap<Customer, CustomerDetailViewModel>().ForMember(cust => cust.Order, opt => opt.MapFrom(src => src.Order.Customer));
            CreateMap<Customer, CustomerViewModel>().ForMember(dest => dest.FavoriteMovie, opt => opt.MapFrom(src => src.Movie.Title));
            CreateMap<Customer, CustomerDetailViewModel>();
            CreateMap<CreateCustomerModel, Customer>();


            //Actor
            CreateMap<CreateActorModel, Actor>();
            CreateMap<Actor, ActorDetailViewModel>().ForMember(dest => dest.CastMovie, opt => opt.MapFrom(src => src.Movie.Title));
            CreateMap<Actor, ActorViewModel>().ForMember(dest => dest.CastMovie, opt => opt.MapFrom(src => src.Movie.Title));


            //Director
            CreateMap<CreateDirectorModel, Director>();
            CreateMap<Director, DirectorDetailViewModel>().ForMember(dest => dest.DirectedMovie, opt => opt.MapFrom(src => src.Movie.Title));
            CreateMap<Director, DirectorViewModel>().ForMember(dest => dest.DirectedMovie, opt => opt.MapFrom(src => src.Movie.Title));


            //Order
            CreateMap<Order, OrderDetailViewModel>().ForMember(ord => ord.Movie, opt => opt.MapFrom(src => src.Movie.Title));
            CreateMap<Order, OrderDetailViewModel>().ForMember(cust => cust.Customer, opt => opt.MapFrom(src => src.Customer.FirstName));
            CreateMap<Order, OrderDetailViewModel>();
            CreateMap<CreateOrderModel, Order>();
        }
    }
}

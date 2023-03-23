using AutoMapper;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.Application.ActorOperations.Queries.GetActors;
using WebApi.Application.ActorOperations.Queries.Shared;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using WebApi.Application.CustomerOperations.Queries.GetCustomers;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.Application.DirectorOperations.Queries.GetDirectors;
using WebApi.Application.DirectorOperations.Queries.Shared;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.Application.MovieOperations.Queries.GetMovies;
using WebApi.Application.MovieOperations.Queries.Shared;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using WebApi.Application.OrderOperations.Queries.GetOrders;
using WebApi.Entities;
using static WebApi.Application.ActorOperations.Commands.CreateActor.CreateActorCommand;
using static WebApi.Application.DirectorOperations.Commands.CreateDirector.CreateDirectorCommand;
using static WebApi.Application.MovieOperations.Commands.CreateMovie.CreateMovieCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Movie
            CreateMap<CreateMovieModel, Movie>();

            CreateMap<Movie, MovieDetailViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.FirstName + " " + src.Director.LastName));

            CreateMap<Actor, ActorsViewModel>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

            CreateMap<Movie, MovieViewModel>()
             .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
        .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.FirstName + " " + src.Director.LastName));

            CreateMap<Movie, ActedInMovieViewModel>()
                  .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
        .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.FirstName + " " + src.Director.LastName));

            //Genre
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GetGenreDetailViewModel>();

            //Customer
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Customer, CustomerDetailViewModel>();
            CreateMap<CreateCustomerModel, Customer>().ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            //Actor
            CreateMap<CreateActorModel, Actor>();
            CreateMap<Actor, ActorDetailViewModel>();
            CreateMap<Actor, ActorViewModel>();
            CreateMap<Actor, ActedInMovieViewModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));


            //Director
            CreateMap<Director, DirectorViewModel>();
            CreateMap<Director, DirectorDetailViewModel>();
            CreateMap<CreateDirectorModel, Director>();
            CreateMap<Movie, DirectedMovieViewModel>()
             .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

            //Order
            CreateMap<Order, OrderDetailViewModel>()
                .ForMember(ord => ord.Movie, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(cust => cust.Customer, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));
            CreateMap<CreateOrderModel, Order>();
            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer.FirstName +" " + src.Customer.LastName));
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieStoreDbContext
            (serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                if (context.Movies.Any())
                {
                    return;
                }

                //Genre

                var genre1 = new Genre { Name = "Romantik Komedi" };
                var genre2 = new Genre { Name = "Komedi" };
                var genre3 = new Genre { Name = "Bilim Kurgu" };
                var genre4 = new Genre { Name = "Dram" };
                var genre5 = new Genre { Name = "Aksiyon" };
                var genre6 = new Genre { Name = "Korku" };
                var genre7 = new Genre { Name = "Polisiye" };

                //Director
                context.Directors.AddRange(
                new Director { FirstName = "Zeynep", LastName = "Güral" },
                new Director { FirstName = "Nazan", LastName = "Somuncu" },
                new Director { FirstName = "Hakkı", LastName = "Songül" },
                new Director { FirstName = "Murat", LastName = "Altın" },
                new Director { FirstName = "Arif", LastName = "Demirci" }

                );
                
                //Movies
                var movie1 = new Movie
                {
                    Title = "Ay Tutulması",
                    GenreId = 1,
                    DirectorId = 2,
                    PublicationDate = new DateTime(2001, 06, 12),
                    Price = 60
                };
                var movie2 = new Movie
                {
                    Title = "Yıldızların Altında",
                    GenreId = 2,
                    DirectorId = 4,
                    PublicationDate = new DateTime(2010, 05, 23),
                    Price = 50
                };
                var movie3 = new Movie
                {
                    Title = "Güneşin Doğuşu",
                    GenreId = 2,
                    DirectorId = 3,
                    PublicationDate = new DateTime(2001, 12, 21),
                    Price = 65
                };
                var movie4 = new Movie
                {
                    Title = "Gün Batımı",
                    GenreId = 4,
                    DirectorId = 1,
                    PublicationDate = new DateTime(2001, 12, 21),
                    Price = 70
                };
                var movie5 = new Movie
                {
                    Title = "Gökyüzündeki Yıldız Takımı",
                    GenreId = 1,
                    DirectorId = 5,
                    PublicationDate = new DateTime(2024, 08, 05),
                    Price = 75
                };

                //Order

                var order1 = new Order { MovieId = 5, CustomerId = 1, Price = movie1.Price, OrderDate = new DateTime(2023, 01, 08) };
                var order2 = new Order { MovieId = 4, CustomerId = 2, Price = movie2.Price, OrderDate = new DateTime(2023, 02, 10) };
                var order3 = new Order { MovieId = 3, CustomerId = 3, Price = movie3.Price, OrderDate = new DateTime(2023, 03, 15) };
                var order4 = new Order { MovieId = 2, CustomerId = 4, Price = movie4.Price, OrderDate = new DateTime(2023, 01, 22) };
                var order5 = new Order { MovieId = 1, CustomerId = 5, Price = movie5.Price, OrderDate = new DateTime(2023, 03, 29) };


                //Actor
                context.Actors.AddRange(
                new Actor
                {
                    ActorMovies = new List<Movie> { movie1, movie2 },
                    FirstName = "Salih",
                    LastName = "Gündüz"
                },
                new Actor
                {
                    ActorMovies = new List<Movie> { movie2, movie3 },
                    FirstName = "Gamze",
                    LastName = "Ay"
                },
                new Actor
                {
                    ActorMovies = new List<Movie> { movie4, movie1 },
                    FirstName = "Leyla",
                    LastName = "Yıldız"
                },
                new Actor
                {
                    ActorMovies = new List<Movie> { movie5, movie1 },
                    FirstName = "Fırat",
                    LastName = "Kalkan"
                },
                new Actor
                {
                    ActorMovies = new List<Movie> { movie4, movie5 },
                    FirstName = "Ali",
                    LastName = "Yılmaz"
                },
                new Actor
                {
                    ActorMovies = new List<Movie> { movie2, movie3 },
                    FirstName = "Veli",
                    LastName = "Günay"
                }

                );

                //Customer
                context.Customers.AddRange(
                new Customer
                {
                    FirstName = "Halil",
                    LastName = "Demirer",
                    Email = "halild@halil.com",
                    Password = "halil123",
                    Orders = new List<Order> { order1 },
                    FavoriteGenres = new List<Genre> { genre5, genre3}
                },
                new Customer
                {
                    FirstName = "Buse",
                    LastName = "Çamlı",
                    Email = "busec@buse.com",
                    Password = "buse123",
                    Orders = new List<Order> { order2},
                    FavoriteGenres = new List<Genre> { genre4, genre1 }
                },
                new Customer
                {
                    FirstName = "Tülay",
                    LastName = "Çanakçı",
                    Email = "tulayc@cemal.com",
                    Password = "tulay123",
                    Orders = new List<Order> { order3 },
                    FavoriteGenres = new List<Genre> { genre2, genre6 }

                },
                new Customer
                {
                    FirstName = "Fatih",
                    LastName = "Çömlekçi",
                    Email = "fatihc@fatih.com",
                    Password = "fatih123",
                    Orders = new List<Order> { order4 },
                    FavoriteGenres = new List<Genre> { genre4, genre1 }
                },
                new Customer
                {
                    FirstName = "Cemal",
                    LastName = "Taş",
                    Email = "cemalt@cemal.com",
                    Password = "cemal123",
                    Orders = new List<Order> { order5 },
                    FavoriteGenres = new List<Genre> { genre7, genre5 }
                }
                );
                context.SaveChanges();
            }
        }
    }
}

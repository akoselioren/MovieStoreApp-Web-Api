using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WebApi.Entities;
using static System.Collections.Specialized.BitVector32;

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
                context.Genres.AddRange(
                new Genre
                {
                    Name = "Romantik Komedi"
                },
                new Genre
                {
                    Name = "Komedi"
                },
                new Genre
                {
                    Name = "Bilim Kurgu"
                },
                new Genre
                {
                    Name = "Drama"
                },
                new Genre
                {
                    Name = "Aksiyon"
                },
                new Genre
                {
                    Name = "Korku"
                },
                new Genre
                {
                    Name = "Polisiye"
                }

                );
                //Actor
                context.Actors.AddRange(
                new Actor
                {
                    CastMovieId= 1,
                    FirstName = "Salih",
                    LastName = "Gündüz"
                },
                new Actor
                {
                    CastMovieId = 1,
                    FirstName = "Gamze",
                    LastName = "Ay"
                },
                new Actor
                {
                    CastMovieId = 1,
                    FirstName = "Leyla",
                    LastName = "Yıldız"
                }, 
                new Actor
                {
                    CastMovieId = 1,
                    FirstName = "Fırat",
                    LastName = "Kalkan"
                },
                new Actor
                {
                    CastMovieId = 2,
                    FirstName = "Ali",
                    LastName = "Yılmaz"
                },
                new Actor
                {
                    CastMovieId = 2,
                    FirstName = "Veli",
                    LastName = "Günay"
                }

                );
                //Director
                context.Directors.AddRange(
                new Director
                {
                    DirectedMovieId = 1,
                    FirstName = "Zeynep",
                    LastName = "Güral"
                },
                new Director
                {
                    DirectedMovieId = 2,
                    FirstName = "Nazan",
                    LastName = "Somuncu"
                },
                new Director
                {
                    DirectedMovieId = 3,
                    FirstName = "Hakkı",
                    LastName = "Songül"
                },
                new Director
                {
                    DirectedMovieId = 4,
                    FirstName = "Murat",
                    LastName = "Altın"
                },
                new Director
                {
                    DirectedMovieId = 5,
                    FirstName = "Arif",
                    LastName = "Demirci"
                }

                );
                //Customer
                context.Customers.AddRange(
                new Customer
                {
                    FavoriteMovieId = 5,
                    FirstName = "Halil",
                    LastName = "Konak",
                    PhoneNumber = "05554443322"
                },
                new Customer
                {
                    FavoriteMovieId = 4,
                    FirstName = "Buse",
                    LastName = "Bardak",
                    PhoneNumber = "05553332211"
                },
                new Customer
                {
                    FavoriteMovieId = 1,
                    FirstName = "Tülay",
                    LastName = "Çanakçı",
                    PhoneNumber = "05543332211"
                },
                new Customer
                {
                    FavoriteMovieId = 2,
                    FirstName = "Fatih",
                    LastName = "Çömlekçi",
                    PhoneNumber = "05532221100"
                },
                new Customer
                {
                    FavoriteMovieId = 1,
                    FirstName = "Cemal",
                    LastName = "Taş",
                    PhoneNumber = "05443332210"
                }
                );

                //Order
                context.Orders.AddRange(
                new Order
                {
                    MovieId = 5,
                    CustomerId = 1,
                    Price = 15,
                    OrderDate = new DateTime(2023, 01, 08)
                },
                new Order
                {
                    MovieId = 4,
                    CustomerId = 2,
                    Price = 70,
                    OrderDate = new DateTime(2023, 02, 10)
                },
                new Order
                {
                    MovieId = 3,
                    CustomerId = 3,
                    Price = 65,
                    OrderDate = new DateTime(2023, 03, 15)
                },
                new Order
                {
                    MovieId = 2,
                    CustomerId = 4,
                    Price = 50,
                    OrderDate = new DateTime(2023, 01, 22)
                },
                new Order
                {
                    MovieId = 1,
                    CustomerId = 5,
                    Price = 60,
                    OrderDate = new DateTime(2023, 03, 29)
                }


                );
                //Movies
                context.Movies.AddRange(
            new Movie
            {
                Title = "Ay Tutulması",
                GenreId = 1,
                DirectorId = 4,
                RunningTime = "02:45:05",
                PublicationDate = new DateTime(2001, 06, 12),
                Price = 60
            },
           new Movie
           {
               Title = "Yıldızların Altında",
               GenreId = 2,
               DirectorId = 2,
               RunningTime = "02:25:45",
               PublicationDate = new DateTime(2010, 05, 23),
               Price= 50
           },
           new Movie
           {
               Title = "Güneşin Doğuşu",
               GenreId = 2,
               DirectorId = 3,
               RunningTime = "02:40:55",
               PublicationDate = new DateTime(2001, 12, 21),
               Price = 65
           },
            new Movie
            {
                Title = "Gün Batımı",
                GenreId = 4,
                DirectorId = 1,
                RunningTime = "02:08:00",
                PublicationDate = new DateTime(2001, 12, 21),
                Price = 70
            },
             new Movie
             {
                 Title = "Gökyüzündeki Yıldız Takımı",
                 GenreId = 1,
                 DirectorId = 5,
                 RunningTime = "01:58:45",
                 PublicationDate = new DateTime(2024, 08, 05),
                 Price = 75
             });

                context.SaveChanges();
            }
        }
    }
}

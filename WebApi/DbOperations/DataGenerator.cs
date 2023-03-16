using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
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
                context.Genres.AddRange(
                new Genre
                {
                    Name = "Science Fiction"
                },
                new Genre
                {
                    Name = "Action"
                },
                new Genre
                {
                    Name = "Comedy"
                },
                new Genre
                {
                    Name = " Drama"
                },
                new Genre
                {
                    Name = "Action-Comedy"
                },
                new Genre
                {
                    Name = "Comedy-Romance"
                },
                new Genre
                {
                    Name = "Crime"
                }

                );

                context.Movies.AddRange(
            new Movie
            {
                Title = "Lean Startup",
                GenreId = 1,
                RunningTime = "02:45:05",
                PublicationDate = new DateTime(2001, 06, 12)
            },
           new Movie
           {
               Title = "Herland",
               GenreId = 2,
               RunningTime = "02:25:45",
               PublicationDate = new DateTime(2010, 05, 23)
           },
           new Movie
           {
               Title = "Dune",
               GenreId = 2,
               RunningTime = "02:40:55",
               PublicationDate = new DateTime(2001, 12, 21)
           },
            new Movie
            {
                Title = "Lord Of The Rings",
                GenreId = 3,
                RunningTime = "02:08:00",
                PublicationDate = new DateTime(2001, 12, 21)
            },
             new Movie
             {
                 Title = "Hobbit",
                 GenreId = 2,
                 RunningTime = "01:58:45",
                 PublicationDate = new DateTime(2024, 08, 05)
             });

                context.SaveChanges();
            }
        }
    }
}

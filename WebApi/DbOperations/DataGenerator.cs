using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

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
                context.Movies.AddRange(
            new Movie
            {
               // Id = 1,
                Title = "Lean Startup",
                GenreId = 1,
                RunningTime = "02:45:05",
                PublicationDate = new DateTime(2001, 06, 12)
            },
           new Movie
           {
              // Id = 2,
               Title = "Herland",
               GenreId = 2,
               RunningTime = "02:25:45",
               PublicationDate = new DateTime(2010, 05, 23)
           },
           new Movie
           {
              // Id = 3,
               Title = "Dune",
               GenreId = 2,
               RunningTime = "02:40:55",
               PublicationDate = new DateTime(2001, 12, 21)
           },
            new Movie
            {
              //  Id = 4,
                Title = "Lord Of The Rings",
                GenreId = 3,
                RunningTime = "02:08:00",
                PublicationDate = new DateTime(2001, 12, 21)
            },
         new Movie
         {
            // Id = 5,
             Title = "Hobbit",
             GenreId = 2,
             RunningTime ="01:58:45",
             PublicationDate = new DateTime(2024, 08, 05)
         });

                context.SaveChanges();
            }
        }
    }
}

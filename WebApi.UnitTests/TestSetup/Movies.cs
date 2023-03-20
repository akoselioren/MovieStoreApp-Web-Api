using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Movies
    {
        public static void AddMovies(this MovieStoreDbContext context)
        {
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
               Price = 50
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
        }
    }
}

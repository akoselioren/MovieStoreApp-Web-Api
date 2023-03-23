using System;
using System.Collections.Generic;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Actors
    {
        public static void AddActors(this MovieStoreDbContext context)
        {
            #region data movie
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
            #endregion


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
        }
    }
}

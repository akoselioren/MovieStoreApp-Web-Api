﻿using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Movies
    {
        public static void AddMovies(this MovieStoreDbContext context)
        {
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
        }
    }
}

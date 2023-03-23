using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Orders
    {
        public static void AddOrders(this MovieStoreDbContext context)
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

            var order1 = new Order { MovieId = 5, CustomerId = 1, Price = movie1.Price, OrderDate = new DateTime(2023, 01, 08) };
            var order2 = new Order { MovieId = 4, CustomerId = 2, Price = movie2.Price, OrderDate = new DateTime(2023, 02, 10) };
            var order3 = new Order { MovieId = 3, CustomerId = 3, Price = movie3.Price, OrderDate = new DateTime(2023, 03, 15) };
            var order4 = new Order { MovieId = 2, CustomerId = 4, Price = movie4.Price, OrderDate = new DateTime(2023, 01, 22) };
            var order5 = new Order { MovieId = 1, CustomerId = 5, Price = movie5.Price, OrderDate = new DateTime(2023, 03, 29) };
        }
    }
}

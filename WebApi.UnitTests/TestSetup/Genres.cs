using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this MovieStoreDbContext context) 
        {
            var genre1 = new Genre { Name = "Romantik Komedi" };
            var genre2 = new Genre { Name = "Komedi" };
            var genre3 = new Genre { Name = "Bilim Kurgu" };
            var genre4 = new Genre { Name = "Dram" };
            var genre5 = new Genre { Name = "Aksiyon" };
            var genre6 = new Genre { Name = "Korku" };
            var genre7 = new Genre { Name = "Polisiye" };
        }
    }
}

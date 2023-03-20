using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this MovieStoreDbContext context) 
        {
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
        }
    }
}

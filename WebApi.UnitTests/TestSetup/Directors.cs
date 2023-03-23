using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Directors
    {
        public static void AddDirectors(this MovieStoreDbContext context)
        {
            context.Directors.AddRange(
               new Director { FirstName = "Zeynep", LastName = "Güral" },
               new Director { FirstName = "Nazan", LastName = "Somuncu" },
               new Director { FirstName = "Hakkı", LastName = "Songül" },
               new Director { FirstName = "Murat", LastName = "Altın" },
               new Director { FirstName = "Arif", LastName = "Demirci" }

               );
        }
    }
}

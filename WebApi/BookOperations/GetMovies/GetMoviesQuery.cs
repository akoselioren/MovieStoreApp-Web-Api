using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetMoviesQuery
    {
        private readonly MovieStoreDbContext _dbContext;

        public GetMoviesQuery(MovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

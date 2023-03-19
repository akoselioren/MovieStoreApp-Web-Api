using System.Linq;
using System;
using WebApi.DbOperations;

namespace WebApi.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        private readonly MovieStoreDbContext _dbContext;
        public int DirectorId { get; set; }

        public DeleteDirectorCommand(MovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var director = _dbContext.Directors.SingleOrDefault(x => x.Id == DirectorId);
            if (director is null)
                throw new InvalidOperationException("Silnecek Director bulunamadı.");

            _dbContext.Directors.Remove(director);
            _dbContext.SaveChanges();
        }

    }
}

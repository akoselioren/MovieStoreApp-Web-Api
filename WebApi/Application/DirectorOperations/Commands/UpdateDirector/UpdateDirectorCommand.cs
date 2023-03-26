using System.Linq;
using System;
using WebApi.DbOperations;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public int DirectorId { get; set; }
        public UpdateDirectorModel Model { get; set; }

        public UpdateDirectorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var director = _dbContext.Directors.SingleOrDefault(x => x.Id == DirectorId);

            if (director is null)
                throw new InvalidOperationException("Güncellenecek Yönetmen'e ulaşılamadı.");

            director.FirstName = string.IsNullOrEmpty(Model.FirstName) ? director.FirstName : Model.FirstName.Trim();
            director.LastName = string.IsNullOrEmpty(Model.LastName) ? director.LastName : Model.LastName.Trim();

            if (_dbContext.Directors.Any(d => d.FirstName.ToLower() == director.FirstName.ToLower() && d.LastName.ToLower() == director.LastName.ToLower() && d.Id != director.Id))
                throw new InvalidOperationException("Bu isimde bir Director zaten var.");

            _dbContext.SaveChanges();
        }

        public class UpdateDirectorModel
        {
            private string firstName;
            public string FirstName
            {
                get { return firstName; }
                set { firstName = value.Trim(); }
            }
            private string lastName;
            public string LastName
            {
                get { return lastName; }
                set { lastName = value.Trim(); }
            }
        }

    }
}

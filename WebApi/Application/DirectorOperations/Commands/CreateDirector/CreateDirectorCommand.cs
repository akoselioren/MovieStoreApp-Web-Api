using AutoMapper;
using System.Linq;
using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommand
    {
        public CreateDirectorModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateDirectorCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var director = _dbContext.Directors.SingleOrDefault(x => x.FirstName == Model.FirstName && x.LastName == Model.LastName);

            if (director is not null)
                throw new InvalidOperationException("Eklemek istediğiniz Yönetmen zaten mevcut");

            director = _mapper.Map<Director>(Model);

            _dbContext.Directors.Add(director);
            _dbContext.SaveChanges();
        }

        public class CreateDirectorModel
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

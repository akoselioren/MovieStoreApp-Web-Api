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
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateDirectorCommand(MovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var director = _dbContext.Directors.SingleOrDefault(x => x.FirstName == Model.FirstName && x.LastName == Model.LastName);

            if (director is not null)
                throw new InvalidOperationException("Eklemek istediğiniz Director zaten mevcut");

            director = _mapper.Map<Director>(Model);

            _dbContext.Directors.Add(director);
            _dbContext.SaveChanges();
        }

        public class CreateDirectorModel
        {
            public int DirectedMovieId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}

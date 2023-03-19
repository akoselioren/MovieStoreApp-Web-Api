using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        public readonly MovieStoreDbContext _context;
        public readonly IMapper _mapper;

        public GetGenreDetailQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetGenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException("Film tür'ü bulunamadı.");

            return _mapper.Map<GetGenreDetailViewModel>(genre);
        }
    }

    public class GetGenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenGenreIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = 321;

            // act & assert
            FluentActions
              .Invoking(() => query.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Film tür'ü bulunamadı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeReturned()
        {
            // arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = 2;

            // act
            FluentActions.Invoking(() => query.Handle()).Invoke();

            // assert
            Genre genre = _context.Genres.SingleOrDefault(genre => genre.Id == query.GenreId);

            genre.Should().NotBeNull();
        }
    }
}

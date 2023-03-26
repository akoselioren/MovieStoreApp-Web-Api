using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.MovieOperations.Queries
{
    public class GetMoviesDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetMoviesDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenMovieIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // arrange
            GetMoviesDetailQuery query = new GetMoviesDetailQuery(_context, _mapper);
            query.MovieId = 555;

            // act & assert
            FluentActions
              .Invoking(() => query.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Film bulunamadı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShouldBeReturned()
        {
            // arrange
            GetMoviesDetailQuery query = new GetMoviesDetailQuery(_context, _mapper);
            query.MovieId = 2;

            // act
            FluentActions.Invoking(() => query.Handle()).Invoke();

            // assert
            Movie movie = _context.Movies.SingleOrDefault(movie => movie.Id == query.MovieId);

            movie.Should().NotBeNull();
        }
    }
}

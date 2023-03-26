using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandTests :IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;

        public DeleteMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }

        [Fact]
        public void WhenGivenMovieIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // arrange
            DeleteMovieCommand command = new DeleteMovieCommand(_dbContext);
            command.MovieId = 101;

            // act & assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Silnecek Film bulunamadı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShouldBeDeleted()
        {
            // arrange
            Movie newMovie = new Movie()
            {
                Title = "A New Movie Name",
                DirectorId = 1,
                GenreId = 1,
                Price = 19
            };

            _dbContext.Movies.Add(newMovie);
            _dbContext.SaveChanges();

            Movie createdMovie = _dbContext.Movies.SingleOrDefault(movie => movie.Title == newMovie.Title);

            DeleteMovieCommand command = new DeleteMovieCommand(_dbContext);
            command.MovieId = createdMovie.Id;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            Movie movie = _dbContext.Movies.SingleOrDefault(movie => movie.Id == createdMovie.Id && movie.IsActive);

            movie.Should().BeNull();
        }
    }
}

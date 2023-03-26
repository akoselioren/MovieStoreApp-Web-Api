using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.MovieOperations.Commands.UpdateMovie.UpdateMovieCommand;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public UpdateMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenMovieIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // Arrange
            UpdateMovieCommand command = new UpdateMovieCommand(_context);
            command.MovieId = 419;
            command.Model = new UpdateMovieModel() {Title= "Updated Movie" };

            // Act & Assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Güncellenecek Film'e ulaşılamadı.");
        }

        [Fact]
        public void WhenGivenMovieNameAlreadyExistsWithDifferentId_Handle_ThrowsInvalidOperationException()
        {
            // Arrange
            UpdateMovieCommand command = new UpdateMovieCommand(_context);
            command.MovieId = 3;
            command.Model = new UpdateMovieModel() {Title= "Güneşin Doğuşu" };

            // Act & Assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Bu isimde bir film zaten var.");
        }

        [Fact]
        public void WhenDefaultInputsAreGiven_Movie_ShouldNotBeChanged()
        {
            // arrange
            Movie newMovie = new Movie()
            {
                Title = "A New Movie",
                Price = 22,
                GenreId = 1,
                DirectorId = 1,
                PublicationDate = new DateTime(2008, 06, 12)
            };
            _context.Movies.Add(newMovie);
            _context.SaveChanges();
            Movie addedMovie = _context.Movies.SingleOrDefault(movie => movie.Title.ToLower() == newMovie.Title.ToLower());

            UpdateMovieCommand command = new UpdateMovieCommand(_context);
            command.MovieId = addedMovie.Id;
            UpdateMovieModel model = new UpdateMovieModel()
            {
               Title= "",
                Price = 0,
                GenreId = 0,
                DirectorId = 0,
                PublicationDate = new DateTime(2008, 06, 12)
            };
            command.Model = model;

            Movie movieBeforeUpdate = _context.Movies.SingleOrDefault(movie => movie.Id == command.MovieId);

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            Movie movieAfterUpdate = _context.Movies.SingleOrDefault(movie => movie.Id == command.MovieId);

            movieAfterUpdate.Should().NotBeNull();
            movieAfterUpdate.Title.Should().Be(movieBeforeUpdate.Title);
            movieAfterUpdate.Price.Should().Be(movieBeforeUpdate.Price);
            movieAfterUpdate.GenreId.Should().Be(movieBeforeUpdate.GenreId);
            movieAfterUpdate.DirectorId.Should().Be(movieBeforeUpdate.DirectorId);
            movieAfterUpdate.PublicationDate.Should().Be(movieBeforeUpdate.PublicationDate);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShouldBeUpdated()
        {
            // arrange
            UpdateMovieCommand command = new UpdateMovieCommand(_context);
            command.MovieId = 3;
            UpdateMovieModel model = new UpdateMovieModel()
            {
               Title= "Updated Movie Name",
                Price = 12,
                PublicationDate = new DateTime(2008, 06, 12),
                GenreId = 1,
                DirectorId = 2
            };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            Movie movie = _context.Movies.SingleOrDefault(movie => movie.Id == command.MovieId);

            movie.Should().NotBeNull();
            movie.Title.Should().Be(model.Title);
            movie.Price.Should().Be(model.Price);
            movie.GenreId.Should().Be(model.GenreId);
            movie.DirectorId.Should().Be(model.DirectorId);
            movie.PublicationDate.Should().Be(model.PublicationDate);
        }
    }
}

using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public DeleteGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenGenreIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 452;

            // act & assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Film Türü Bulunamadı.");
        }

        [Fact]
        public void WhenGivenGenreIsCurrentlyPlayingInAMovie_Handle_ThrowsInvalidOperationException()
        {
            // arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 456;

            // act & assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Film Türü Bulunamadı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeDeleted()
        {
            // arrange
            Genre genreWithNoMovies = new Genre()
            {
                Name = "Genre with",
            };

            _context.Genres.Add(genreWithNoMovies);
            _context.SaveChanges();

            Genre createdGenre = _context.Genres.SingleOrDefault(genre => ((genre.Name.ToLower()) == (genreWithNoMovies.Name.ToLower())));

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = createdGenre.Id;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            Genre genre = _context.Genres.SingleOrDefault(genres => genres.Id == command.GenreId);

            genre.Should().BeNull();
        }
    }
}

using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

using static WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;
namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public UpdateGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenGenreIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 987;
            command.Model = new UpdateGenreModel() { Name = "Deneme Genre"};

            // Act & Assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Film Türü Bulunamadı.");
        }

        [Fact]
        public void WhenGivenGenreNameAlreadyExistsWithDifferentId_Handle_ThrowsInvalidOperationException()
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 198;
            command.Model = new UpdateGenreModel() { Name = "Güncel tür deneme"};

            // Act & Assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().NotBeNull("Aynı ad'a sahip tür zaten mevcuttur.");
        }

        [Fact]
        public void WhenDefaultInputsAreGiven_Genre_ShouldNotBeChanged()
        {
            // arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 2;
            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = ""
            };
            command.Model = model;

            Genre genreBeforeUpdate = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            Genre genreAfterUpdate = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);

            genreAfterUpdate.Should().NotBeNull();
            genreAfterUpdate.Name.Should().Be(genreBeforeUpdate.Name);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            // arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 2;
            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = "Güncelle Tür Deneme"

            };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            Genre genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);

            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
        }
    }
}

using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Fact]
        public void WhenAlreadyExistingGenreNameIsGiven_Handle_ThrowsInvalidOperationException()
        {
            Genre genre = new Genre()
            {
                Name = "Çizgi Film"
            };
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();

            // arrange
            CreateGenreCommand command = new CreateGenreCommand(_dbContext);
            command.Model = new CreateGenreModel()
            {
                Name = genre.Name
            };

            // act & assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Film türü zaten mevcut.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            // arrange
            CreateGenreCommand command = new CreateGenreCommand(_dbContext);
            var model = new CreateGenreModel()
            {
                Name = "Çizgi"
            };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var genre = _dbContext.Genres.SingleOrDefault(genre => genre.Name.ToLower() == model.Name.ToLower());

            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
        }

    }
}

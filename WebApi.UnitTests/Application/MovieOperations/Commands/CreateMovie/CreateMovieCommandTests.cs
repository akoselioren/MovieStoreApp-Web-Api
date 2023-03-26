using AutoMapper;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.MovieOperations.Commands.CreateMovie.CreateMovieCommand;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.CreateCommand
{
    public class CreateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistMovieTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)
            var movie = new Movie()
            {
                Title = "Sahil Kasabası",
                GenreId = 1,
                DirectorId = 2,
                PublicationDate = new DateTime(2008, 06, 12),
                Price = 60
            };
            _context.Add(movie);
            _context.SaveChanges();

            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            command.Model = new CreateMovieModel() { Title = movie.Title };
            //act (Çalıştırma)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film zaten mevcut");
            //assert (Doğrulama)
        }

        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShouldCreated()
        {
            //Arrange
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            CreateMovieModel model = new CreateMovieModel()
            {
                Title = "Dağlardan Gelen Ses",
                GenreId = 1,
                DirectorId = 2,
                PublicationDate = DateTime.Now.Date.AddYears(-10),
                Price = 90
            };
            command.Model = model;

            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert
            var movie = _context.Movies.SingleOrDefault(movie => movie.Title.ToLower() == model.Title.ToLower());
            movie.Should().NotBeNull();
            movie.GenreId.Should().Be(model.GenreId);
            movie.DirectorId.Should().Be(model.DirectorId);
            movie.PublicationDate.Should().Be(model.PublicationDate);
            movie.Price.Should().Be(model.Price);
        }
    }
}

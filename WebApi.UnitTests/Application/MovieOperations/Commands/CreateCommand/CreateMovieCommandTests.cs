using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Title = "WhenAlreadyExistMovieTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                GenreId = 2,
                RunningTime = "01:45:05",
                PublicationDate = new DateTime(2001, 06, 12),
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
                Title = "Hobbit",
                GenreId = 2,
                RunningTime = "01:45:05",
                PublicationDate = DateTime.Now.Date.AddYears(-10)
            };
            command.Model = model;

            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert
            var movie = _context.Movies.SingleOrDefault(movie => movie.Title == model.Title);
            movie.Should().NotBeNull();
            movie.GenreId.Should().Be(model.GenreId);
            movie.RunningTime.Should().Be(model.RunningTime);
            movie.PublicationDate.Should().Be(model.PublicationDate);
        }


    }
}

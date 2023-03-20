using AutoMapper;
using FluentAssertions;
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
    public class CreateMovieCommandTestsValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord ",0, "")]
        [InlineData("Lord Of",0, "")]
        [InlineData("Lord Of The",0, "01:25:05")]
        [InlineData("Lord Of The Rings",0, "")]
        [InlineData("Lord Of The Ringse",0, "01:25:55")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title,int GenreId, string RunningTime)
        {   //Arrange
            CreateMovieCommand command = new CreateMovieCommand(null, null);
            command.Model = new CreateMovieModel()
            {
                Title = title,
                GenreId = GenreId,
                RunningTime = RunningTime,
                PublicationDate = new DateTime(2001, 06, 12)
            };
            //Act
            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateMovieCommand command= new CreateMovieCommand(null, null);
            command.Model = new CreateMovieModel()
            {
                Title = "Lord Of The Rings",
                GenreId = 2,
                RunningTime = "02:05:00",
                PublicationDate = DateTime.Now.Date
            };
            CreateMovieCommandValidator validator= new CreateMovieCommandValidator();
            var result=validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateMovieCommand command = new CreateMovieCommand(null, null);
            command.Model = new CreateMovieModel()
            {
                Title = "Lord Of The Rings",
                GenreId = 2,
                RunningTime = "02:05:00",
                PublicationDate = DateTime.Now.Date.AddYears(-2)
            };
            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.MovieOperations.Queries
{
    public class GetMoviesDetailQueryTestsValidator : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
        {
            // arrange
            GetMoviesDetailQuery query = new GetMoviesDetailQuery(null, null);
            query.MovieId = 0;

            // act
            GetMoviesDetailQueryValidator validator = new GetMoviesDetailQueryValidator();
            var validationResult = validator.Validate(query);

            // // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            GetMoviesDetailQuery query = new GetMoviesDetailQuery(null, null);
            query.MovieId = 1;

            // act
            GetMoviesDetailQueryValidator validator = new GetMoviesDetailQueryValidator();
            var validationResult = validator.Validate(query);

            // // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}

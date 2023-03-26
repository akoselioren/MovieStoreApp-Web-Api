using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
        {
            // arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = 0;

            // act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var validationResult = validator.Validate(query);

            // // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = 1;

            // act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var validationResult = validator.Validate(query);

            // // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}

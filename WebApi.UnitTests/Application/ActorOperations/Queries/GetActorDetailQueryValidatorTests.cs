using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.ActorOperations.Queries
{
    public class GetActorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
        {
            // arrange
            GetActorDetailQuery query = new GetActorDetailQuery(null, null);
            query.ActorId = 0;

            // act
            GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
            var validationResult = validator.Validate(query);

            // // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            GetActorDetailQuery query = new GetActorDetailQuery(null, null);
            query. ActorId = 1;

            // act
            GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
            var validationResult = validator.Validate(query);

            // // assert
            validationResult.Errors.Count.Should().Be(0);
        }

    }
}

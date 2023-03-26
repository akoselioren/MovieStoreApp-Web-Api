using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.DirectorOperations.Queries
{
    public class GetDirectorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
        {
            // arrange
            GetDirectorsDetailQuery query = new GetDirectorsDetailQuery(null, null);
            query.DirectorId = 0;

            // act
            GetDirectorsDetailQueryValidator validator = new GetDirectorsDetailQueryValidator();
            var validationResult = validator.Validate(query);

            // // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            GetDirectorsDetailQuery query = new GetDirectorsDetailQuery(null, null);
            query.DirectorId = 1;

            // act
            GetDirectorsDetailQueryValidator validator = new GetDirectorsDetailQueryValidator();
            var validationResult = validator.Validate(query);

            // // assert
            validationResult.Errors.Count.Should().Be(0);
        }

    }
}

using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.CustomerOperations.Queries
{
    public class GetCustomerDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
        {
            // arrange
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(null, null);
            query.CustomerId = 0;

            // act
            GetCustomerDetailQueryValidator validator = new GetCustomerDetailQueryValidator();
            var validationResult = validator.Validate(query);

            // // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(null, null);
            query.CustomerId = 1;

            // act
            GetCustomerDetailQueryValidator validator = new GetCustomerDetailQueryValidator();
            var validationResult = validator.Validate(query);

            // // assert
            validationResult.Errors.Count.Should().Be(0);
        }

    }
}

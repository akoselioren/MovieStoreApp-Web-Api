using FluentAssertions;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.OrderOperations.Queries
{
    public class GetOrderDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
        {
            // arrange
            GetOrderDetailQuery query = new GetOrderDetailQuery(null, null);
            query.OrderId = 0;

            // act
            GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
            var validationResult = validator.Validate(query);

            // // assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            GetOrderDetailQuery query = new GetOrderDetailQuery(null, null);
            query.OrderId = 1;

            // act
            GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
            var validationResult = validator.Validate(query);

            // // assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}

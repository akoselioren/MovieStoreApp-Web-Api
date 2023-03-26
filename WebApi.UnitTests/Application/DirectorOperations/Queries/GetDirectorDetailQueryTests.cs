using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.DirectorOperations.Queries
{
    public class GetDirectorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetDirectorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenGivenDirectorIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // arrange
            GetDirectorsDetailQuery query = new GetDirectorsDetailQuery(_context, _mapper);
            query.DirectorId = 777;

            // act & assert
            FluentActions
              .Invoking(() => query.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Yönetmen bulunamadı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeReturned()
        {
            // arrange
            GetDirectorsDetailQuery query = new GetDirectorsDetailQuery(_context, _mapper);
            query.DirectorId = 2;

            // act
            FluentActions.Invoking(() => query.Handle()).Invoke();

            // assert
            Director director = _context.Directors.SingleOrDefault(director => director.Id == query.DirectorId);

            director.Should().NotBeNull();
        }
    }
}

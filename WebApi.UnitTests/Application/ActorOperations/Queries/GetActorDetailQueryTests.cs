using AutoMapper;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.ActorOperations.Queries
{
    public class GetActorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetActorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Fact]
        public void WhenGivenActorIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // arrange
            GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
            query.ActorId =444;

            // act & assert
            FluentActions
              .Invoking(() => query.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Oyuncu bulunamadı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeReturned()
        {
            // arrange
            GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
            query.ActorId = 2;

            // act
            FluentActions.Invoking(() => query.Handle()).Invoke();

            // assert
            Actor author = _context.Actors.SingleOrDefault(author => author.Id == query.ActorId);

            author.Should().NotBeNull();
        }

    }
}

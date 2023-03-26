using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.ActorOperations.Commands.DeleteActor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenActorIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // arrange
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = 222;

            // act & assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Silnecek Oyuncu bulunamadı.");
        }

        [Fact]
        public void WhenGivenActorIsCurrentlyPlayingInAMovie_Handle_ThrowsInvalidOperationException()
        {
            // arrange
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = 222;

            // act & assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should();
        }

        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeDeleted()
        {
            // arrange
            Actor actorWithNoMovies = new Actor()
            {
                FirstName = "Actor with",
                LastName = "No movie",
            };

            _context.Actors.Add(actorWithNoMovies);
            _context.SaveChanges();

            Actor createdActor = _context.Actors.SingleOrDefault(author => ((author.FirstName.ToLower() + " " + author.LastName.ToLower()) == (actorWithNoMovies.FirstName.ToLower() + " " + actorWithNoMovies.LastName.ToLower())));

            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = createdActor.Id;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            Actor author = _context.Actors.SingleOrDefault(author => author.Id == command.ActorId);

            author.Should().BeNull();
        }
    }
}

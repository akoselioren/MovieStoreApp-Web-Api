using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.ActorOperations.Commands.UpdateActor.UpdateActorCommand;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public UpdateActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenActorIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // Arrange
            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = 121;
            command.Model = new UpdateActorModel() { FirstName = "Updated", LastName = "Actor" };

            // Act & Assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Güncellenecek Actor'e ulaşılamadı.");
        }

        [Fact]
        public void WhenGivenActorNameAlreadyExistsWithDifferentId_Handle_ThrowsInvalidOperationException()
        {
            // Arrange
            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = 122;
            command.Model = new UpdateActorModel() { FirstName = "Ali", LastName = "Yılmaz" };

            // Act & Assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Güncellenecek Actor'e ulaşılamadı.");
        }

        [Fact]
        public void WhenDefaultInputsAreGiven_Actor_ShouldNotBeChanged()
        {
            // arrange
            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = 2;
            UpdateActorModel model = new UpdateActorModel()
            {
                FirstName = "",
                LastName = ""
            };
            command.Model = model;

            Actor authorBeforeUpdate = _context.Actors.SingleOrDefault(author => author.Id == command.ActorId);

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            Actor authorAfterUpdate = _context.Actors.SingleOrDefault(author => author.Id == command.ActorId);

            authorAfterUpdate.Should().NotBeNull();
            authorAfterUpdate.FirstName.Should().Be(authorBeforeUpdate.FirstName);
            authorAfterUpdate.LastName.Should().Be(authorBeforeUpdate.LastName);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeUpdated()
        {
            // arrange
            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = 2;
            UpdateActorModel model = new UpdateActorModel()
            {
                FirstName = "Updated",
                LastName = "Actor Name",

            };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            Actor author = _context.Actors.SingleOrDefault(author => author.Id == command.ActorId);

            author.Should().NotBeNull();
            author.FirstName.Should().Be(model.FirstName);
            author.LastName.Should().Be(model.LastName);
        }
    }
}

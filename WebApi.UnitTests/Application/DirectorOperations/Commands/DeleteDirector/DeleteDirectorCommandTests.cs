using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenDirectorIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // arrange
            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = 876;

            // act & assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Silnecek Yönetici bulunamadı.");
        }

        [Fact]
        public void WhenGivenDirectorIsDirectingAMovie_Handle_ThrowsInvalidOperationException()
        {
            // arrange
            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = 2;

            // act & assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Bu yönetmen bir film yönetmektedir, şu anda silinemez.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeDeleted()
        {
            // arrange
            Director actorWithNoMovies = new Director()
            {
                FirstName = "Direktör ile",
                LastName = "Direktör",
            };

            _context.Directors.Add(actorWithNoMovies);
            _context.SaveChanges();

            Director createdDirector = _context.Directors.SingleOrDefault(author => ((author.FirstName.ToLower() + " " + author.LastName.ToLower()) == (actorWithNoMovies.FirstName.ToLower() + " " + actorWithNoMovies.LastName.ToLower())));

            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = createdDirector.Id;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            Director author = _context.Directors.SingleOrDefault(author => author.Id == command.DirectorId);

            author.Should().BeNull();
        }
    }
}

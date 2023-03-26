using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.DirectorOperations.Commands.UpdateDirector.UpdateDirectorCommand;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public UpdateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }


        [Fact]
        public void WhenGivenDirectorIsNotFound_Handle_ThrowsInvalidOperationException()
        {
            // Arrange
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = 432;
            command.Model = new UpdateDirectorModel() { FirstName = "Updated", LastName = "Director" };

            // Act & Assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Güncellenecek Yönetmen'e ulaşılamadı.");
        }

        [Fact]
        public void WhenGivenDirectorNameAlreadyExistsWithDifferentId_Handle_ThrowsInvalidOperationException()
        {
            // Arrange
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = 443;
            command.Model = new UpdateDirectorModel() { FirstName = "Quentin", LastName = "Tarantino" };

            // Act & Assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Güncellenecek Yönetmen'e ulaşılamadı.");
        }

        [Fact]
        public void WhenDefaultInputsAreGiven_Director_ShouldNotBeChanged()
        {
            // arrange
            Director newDirector = new Director() { FirstName = "A new", LastName = "Director name" };
            _context.Directors.Add(newDirector);
            _context.SaveChanges();
            Director addedDirector = _context.Directors.SingleOrDefault(director => director.FirstName.ToLower() == newDirector.FirstName.ToLower() && director.LastName.ToLower() == newDirector.LastName.ToLower());

            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = addedDirector.Id;
            UpdateDirectorModel model = new UpdateDirectorModel()
            {
                FirstName = "",
                LastName = ""
            };
            command.Model = model;

            Director directorBeforeUpdate = _context.Directors.SingleOrDefault(director => director.Id == command.DirectorId);

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            Director directorAfterUpdate = _context.Directors.SingleOrDefault(director => director.Id == command.DirectorId);

            directorAfterUpdate.Should().NotBeNull();
            directorAfterUpdate.FirstName.Should().Be(directorBeforeUpdate.FirstName);
            directorAfterUpdate.LastName.Should().Be(directorBeforeUpdate.LastName);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeUpdated()
        {
            // arrange
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = 3;
            UpdateDirectorModel model = new UpdateDirectorModel()
            {
                FirstName = "Updated",
                LastName = "Director Name",

            };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            Director director = _context.Directors.SingleOrDefault(director => director.Id == command.DirectorId);

            director.Should().NotBeNull();
            director.FirstName.Should().Be(model.FirstName);
            director.LastName.Should().Be(model.LastName);
        }


    }
}

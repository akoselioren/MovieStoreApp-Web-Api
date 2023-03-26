using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.ActorOperations.Commands.CreateActor.CreateActorCommand;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateActorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistingActorNameIsGiven_Handle_ThrowsInvalidOperationException()
        {
            Actor actor = new Actor()
            {
                FirstName = "Existing",
                LastName = "Actor"
            };
            _dbContext.Actors.Add(actor);
            _dbContext.SaveChanges();

            // arrange
            CreateActorCommand command = new CreateActorCommand(_dbContext, _mapper);
            command.Model = new CreateActorModel()
            {
                FirstName = actor.FirstName,
                LastName = actor.LastName
            };
            
            // act & assert
            FluentActions
              .Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>()
              .And
              .Message.Should().Be("Eklemek istediğiniz Oyuncu zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
        {
            // arrange
            CreateActorCommand command = new CreateActorCommand(_dbContext, _mapper);
            var model = new CreateActorModel()
            {
                FirstName = "Yeni",
                LastName = "Aktor"
            };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var actor = _dbContext.Actors.SingleOrDefault(actor => actor.FirstName.ToLower() == model.FirstName.ToLower() && actor.LastName.ToLower() == model.LastName.ToLower());

            actor.Should().NotBeNull();
            actor.FirstName.Should().Be(model.FirstName);
            actor.LastName.Should().Be(model.LastName);
        }
    }
}

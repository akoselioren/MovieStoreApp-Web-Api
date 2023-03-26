using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = 2;
            command.Model = new UpdateGenreModel()
            {
                Name = "Günceller"
            };

            // act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var validationResult = validator.Validate(command);

            // assert
            validationResult.Errors.Count.Should().Be(0);

        }
    }
}

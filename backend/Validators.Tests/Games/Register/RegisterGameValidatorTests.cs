using BacklogClear.Application.UseCases.Games.Register;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Games.Register;

public class RegisterGameValidatorTests
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new RegisterGameValidator();
        var request = RequestRegisterGameJsonBuilder.Build();
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeTrue();
    }
}
using BacklogClear.Application.UseCases.Games.Register;
using BacklogClear.Communication.Enums;
using BacklogClear.Communication.Requests;
using CommonTestUtilities.Requests;

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
        Assert.True(result.IsValid);
    }
}
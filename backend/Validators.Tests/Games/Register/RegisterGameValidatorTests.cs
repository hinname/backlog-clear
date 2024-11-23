using BacklogClear.Application.UseCases.Games.Register;
using BacklogClear.Communication.Enums;
using BacklogClear.Communication.Requests;

namespace Validators.Tests.Games.Register;

public class RegisterGameValidatorTests
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new RegisterGameValidator();
        var request = new RequestRegisterGameJson()
        {
            Title = "Game",
            Platform = "Description",
            Genre = "Action",
            ReleaseDate = DateTime.Now.AddDays(-1),
            Status = Status.Backlog
        };
        //Act
        var result = validator.Validate(request);
        //Assert
        Assert.True(result.IsValid);
    }
}
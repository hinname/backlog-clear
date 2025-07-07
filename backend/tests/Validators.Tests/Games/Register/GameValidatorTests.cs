using BacklogClear.Application.UseCases.Games.Register;
using BacklogClear.Communication.Enums;
using BacklogClear.Exception.Resources;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Games.Register;

public class GameValidatorTests
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new GameValidator();
        var request = RequestRegisterGameJsonBuilder.Build();
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void ErrorTitleEmpty(string title)
    {
        //Arrange
        var validator = new GameValidator();
        var request = RequestRegisterGameJsonBuilder.Build();
        request.Title = title;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
    }
    
    [Fact]
    public void ErrorPlatformEmpty()
    {
        //Arrange
        var validator = new GameValidator();
        var request = RequestRegisterGameJsonBuilder.Build();
        request.Platform = string.Empty;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PLATFORM_REQUIRED));
    }
    
    [Fact]
    public void ErrorGenreEmpty()
    {
        //Arrange
        var validator = new GameValidator();
        var request = RequestRegisterGameJsonBuilder.Build();
        request.Genre = string.Empty;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.GENRE_REQUIRED));
    }
    
    [Fact]
    public void ErrorDateNotPast()
    {
        //Arrange
        var validator = new GameValidator();
        var request = RequestRegisterGameJsonBuilder.Build();
        request.ReleaseDate = DateTime.UtcNow.AddDays(1);
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.RELEASE_DATE_MUST_BE_IN_PAST));
    }
    
    [Fact]
    public void ErrorStatusTypeInvalid()
    {
        //Arrange
        var validator = new GameValidator();
        var request = RequestRegisterGameJsonBuilder.Build();
        request.Status = (Status)700;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.STATUS_INVALID));
    }

    [Fact]
    public void ErrorStartDateOnBacklog()
    {
        //Arrange
        var validator = new GameValidator();
        var request = RequestRegisterGameJsonBuilder.Build();
        request.Status = Status.Backlog;
        request.StartPlayingDate = DateTime.MinValue;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.START_DATE_NOT_ALLOWED));
    }
    [Fact]
    public void ErrorEndDateOnBacklog()
    {
        //Arrange
        var validator = new GameValidator();
        var request = RequestRegisterGameJsonBuilder.Build();
        request.Status = Status.Backlog;
        request.EndPlayingDate = DateTime.MinValue;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.END_DATE_NOT_ALLOWED));
    }
    [Fact]
    public void ErrorStartDateNotPast()
    {
        //Arrange
        var validator = new GameValidator();
        var request = RequestRegisterGameJsonBuilder.Build();
        request.Status = Status.Playing;
        request.StartPlayingDate = DateTime.UtcNow.AddDays(1);
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.START_DATE_MUST_BE_IN_PAST));
    }
    [Fact]
    public void ErrorEndDateNotPast()
    {
        //Arrange
        var validator = new GameValidator();
        var request = RequestRegisterGameJsonBuilder.Build();
        request.Status = Status.Completed;
        request.EndPlayingDate = DateTime.UtcNow.AddDays(1);
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.END_DATE_MUST_BE_IN_PAST));
    }
}
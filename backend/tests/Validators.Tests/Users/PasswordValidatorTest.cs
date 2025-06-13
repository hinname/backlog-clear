using BacklogClear.Application.UseCases.Users;
using BacklogClear.Communication.Requests.Users;
using FluentAssertions;
using FluentValidation;

namespace Validators.Tests.Users;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData ("")]
    [InlineData (null)]
    [InlineData ("  ")]
    [InlineData ("a")] // minimum length is 8 characters
    [InlineData ("aa")] // minimum length is 8 characters
    [InlineData ("aaa")] // minimum length is 8 characters
    [InlineData ("aaaa")] // minimum length is 8 characters
    [InlineData ("aaaaa")] // minimum length is 8 characters
    [InlineData ("aaaaaa")] // minimum length is 8 characters
    [InlineData ("aaaaaaa")] // minimum length is 8 characters
    [InlineData ("aaaaaaaa")] // needs at least one uppercase letter
    [InlineData ("AAAAAAAA")] // needs at least one lowercase letter
    [InlineData ("Aaaaaaaa")] // needs at least one digit
    [InlineData ("Aaaaaaa1")] // needs at least one special character
    public void ErrorPasswordInvalid(string password)
    {
        //Arrange
        var validator = new PasswordValidator<RequestRegisterUserJson>();
        
        //Act
        var result = validator
            .IsValid(new ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson()), password);
        //Assert
        result.Should().BeFalse();
    }
}
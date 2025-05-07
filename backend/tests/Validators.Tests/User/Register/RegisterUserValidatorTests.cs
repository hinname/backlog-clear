using BacklogClear.Application.UseCases.Users.Register;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.User.Register;

public class RegisterUserValidatorTests
{ 
     [Fact]
     public void Success()
     {
          //Arrange
          var validator = new RegisterUserValidator();
          var request = RequestRegisterUserJsonBuilder.Build();
          //Act
          var result = validator.Validate(request);
          //Assert
          result.IsValid.Should().BeTrue();
     }
     
     [Fact]
     public void ErrorEmailEmpty()
     {
          //Arrange
          var validator = new RegisterUserValidator();
          var request = RequestRegisterUserJsonBuilder.Build();
          request.Email = string.Empty;
          //Act
          var result = validator.Validate(request);
          //Assert
          result.IsValid.Should().BeFalse();
          result.Errors.Should().ContainSingle().And
               .Contain(e => e.ErrorMessage.Equals("Email is required"));
     }
     
     [Fact]
     public void ErrorNicknameEmpty()
     {
          //Arrange
          var validator = new RegisterUserValidator();
          var request = RequestRegisterUserJsonBuilder.Build();
          request.Name = string.Empty;
          //Act
          var result = validator.Validate(request);
          //Assert
          result.IsValid.Should().BeFalse();
          result.Errors.Should().ContainSingle().And
               .Contain(e => e.ErrorMessage.Equals("Nickname is required"));
     }
     
     [Fact]
     public void ErrorPasswordEmpty()
     {
          //Arrange
          var validator = new RegisterUserValidator();
          var request = RequestRegisterUserJsonBuilder.Build();
          request.Password = string.Empty;
          //Act
          var result = validator.Validate(request);
          //Assert
          result.IsValid.Should().BeFalse();
          result.Errors.Should().ContainSingle().And
               .Contain(e => e.ErrorMessage.Equals("Password is required"));
     }
}
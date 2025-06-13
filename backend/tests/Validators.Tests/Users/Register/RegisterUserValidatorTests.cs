using BacklogClear.Application.UseCases.Users.Register;
using BacklogClear.Exception.Resources;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Users.Register;

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
     
     [Theory]
     [InlineData ("")]
     [InlineData (null)]
     [InlineData ("  ")]
     public void ErrorEmailEmpty(string email)
     {
          //Arrange
          var validator = new RegisterUserValidator();
          var request = RequestRegisterUserJsonBuilder.Build();
          request.Email = email;
          //Act
          var result = validator.Validate(request);
          //Assert
          result.IsValid.Should().BeFalse();
          result.Errors.Should().ContainSingle().And
               .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.USER_EMAIL_REQUIRED));
     }

     [Theory]
     [InlineData ("invalid-email")]
     [InlineData ("@invalid.com")]
     [InlineData ("inv@al@id@.com")]
     public void ErrorEmailInvalid(string email)
     {
          //Arrange
          var validator = new RegisterUserValidator();
          var request = RequestRegisterUserJsonBuilder.Build();
          request.Email = email;
          //Act
          var result = validator.Validate(request);
          //Assert
          result.IsValid.Should().BeFalse();
          result.Errors.Should().ContainSingle().And
               .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.USER_EMAIL_INVALID));
     }
     
     [Theory]
     [InlineData ("")]
     [InlineData (null)]
     [InlineData ("  ")]
     public void ErrorNameEmpty(string name)
     {
          //Arrange
          var validator = new RegisterUserValidator();
          var request = RequestRegisterUserJsonBuilder.Build();
          request.Name = name;
          //Act
          var result = validator.Validate(request);
          //Assert
          result.IsValid.Should().BeFalse();
          result.Errors.Should().ContainSingle().And
               .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.USER_NAME_REQUIRED));
     }
     
     [Theory]
     [InlineData ("")]
     [InlineData (null)]
     [InlineData ("  ")]
     public void ErrorPasswordEmpty(string password)
     {
          //Arrange
          var validator = new RegisterUserValidator();
          var request = RequestRegisterUserJsonBuilder.Build();
          request.Password = password;
          //Act
          var result = validator.Validate(request);
          //Assert
          result.IsValid.Should().BeFalse();
          result.Errors.Should().ContainSingle().And
               .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.USER_PASSWORD_REQUIRED));
     }
}
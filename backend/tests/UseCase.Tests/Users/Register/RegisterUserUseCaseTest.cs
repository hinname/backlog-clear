using BacklogClear.Application.UseCases.Users.Register;
using BacklogClear.Exception.ExceptionBase;
using BacklogClear.Exception.Resources;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Users;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCase.Tests.Users.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        // Arrange
        var useCase = CreateUseCase();
        var request = RequestRegisterUserJsonBuilder.Build();

        // Act
        var result = await useCase.Execute(request);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(request.Email);
        result.Token.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public async Task ErrorNameEmpty()
    {
        // Arrange
        var useCase = CreateUseCase();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        // Act
        var act = async () => await useCase.Execute(request);

        // Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex =>
            ex.GetErrorMessages().Count() == 1 &&
            ex.GetErrorMessages().Contains(ResourceErrorMessages.USER_NAME_REQUIRED));
    }
    
    [Fact]
    public async Task ErrorEmailEmpty()
    {
        // Arrange
        var useCase = CreateUseCase();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;

        // Act
        var act = async () => await useCase.Execute(request);

        // Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex =>
            ex.GetErrorMessages().Count() == 1 &&
            ex.GetErrorMessages().Contains(ResourceErrorMessages.USER_EMAIL_REQUIRED));
    }
    
    [Fact]
    public async Task ErrorEmailAndNameEmpty()
    {
        // Arrange
        var useCase = CreateUseCase();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;
        request.Name = string.Empty;

        // Act
        var act = async () => await useCase.Execute(request);

        // Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex =>
            ex.GetErrorMessages().Count() == 2 &&
            ex.GetErrorMessages().Contains(ResourceErrorMessages.USER_EMAIL_REQUIRED) &&
            ex.GetErrorMessages().Contains(ResourceErrorMessages.USER_NAME_REQUIRED));
    }
    
    [Fact]
    public async Task ErrorEmailNameAndPasswordEmpty()
    {
        // Arrange
        var useCase = CreateUseCase();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;
        request.Name = string.Empty;
        request.Password = string.Empty;

        // Act
        var act = async () => await useCase.Execute(request);

        // Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex =>
            ex.GetErrorMessages().Count() == 3 &&
            ex.GetErrorMessages().Contains(ResourceErrorMessages.USER_EMAIL_REQUIRED) &&
            ex.GetErrorMessages().Contains(ResourceErrorMessages.USER_NAME_REQUIRED) &&
            ex.GetErrorMessages().Contains(ResourceErrorMessages.USER_PASSWORD_REQUIRED));
    }

    [Fact]
    public async Task ErrorEmailAlreadyRegistered()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase(request.Email);
        
        var act = async () => await useCase.Execute(request);
        
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex =>
            ex.GetErrorMessages().Count() == 1 &&
            ex.GetErrorMessages().Contains(ResourceErrorMessages.USER_EMAIL_ALREADY_REGISTERED));
    }
    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var usersWriteOnlyRepository = UsersWriteOnlyRepositoryBuilder.Build();
        var usersReadOnlyRepository = new UsersReadOnlyRepositoryBuilder();
        var passwordEncrypter = new PasswordEncrypterBuilder().Build();
        var jwtTokenGenerator = JwtTokenGeneratorBuilder.Build();
        
        if (!string.IsNullOrWhiteSpace(email))
        {
            usersReadOnlyRepository.ExistActiveUserWithEmail(email);
        }
        
        return new RegisterUserUseCase(
            mapper, 
            passwordEncrypter, 
            usersReadOnlyRepository.Build(), 
            usersWriteOnlyRepository, 
            unitOfWork, 
            jwtTokenGenerator);
    }
}
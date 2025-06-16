using BacklogClear.Application.UseCases.Users.Register;
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

    private RegisterUserUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var usersWriteOnlyRepository = UsersWriteOnlyRepositoryBuilder.Build();
        var usersReadOnlyRepository = new UsersReadOnlyRepositoryBuilder().Build();
        var passwordEncrypter = PasswordEncrypterBuilder.Build();
        var jwtTokenGenerator = JwtTokenGeneratorBuilder.Build();
        
        return new RegisterUserUseCase(
            mapper, 
            passwordEncrypter, 
            usersReadOnlyRepository, 
            usersWriteOnlyRepository, 
            unitOfWork, 
            jwtTokenGenerator);
    }
}
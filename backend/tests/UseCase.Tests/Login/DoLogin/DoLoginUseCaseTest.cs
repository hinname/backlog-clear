using BacklogClear.Application.UseCases.Login.DoLogin;
using BacklogClear.Domain.Entities;
using BacklogClear.Exception.ExceptionBase;
using BacklogClear.Exception.Resources;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories.Users;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCase.Tests.Login.DoLogin;

public class DoLoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        
        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;
        var useCase = CreateUseCase(user, request.Password);
        
        var result = await useCase.Execute(request);
        
        result.Should().NotBeNull();
        result.Email.Should().Be(user.Email);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ErrorUserNotFound()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        
        var useCase = CreateUseCase(user, request.Password);
        
        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<InvalidLoginException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 &&
                           ex.GetErrorMessages().Contains(ResourceErrorMessages.LOGIN_EMAIL_OR_PASSWORD_INVALID));
    }
    
    [Fact]
    public async Task ErrorPasswordNotMatch()
    {
        var user = UserBuilder.Build();
        
        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;
        var useCase = CreateUseCase(user);
        
        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<InvalidLoginException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 &&
                           ex.GetErrorMessages().Contains(ResourceErrorMessages.LOGIN_EMAIL_OR_PASSWORD_INVALID));
        
    }

    private DoLoginUseCase CreateUseCase(User user, string? password = null)
    {
        var passwordEncrypter = new PasswordEncrypterBuilder().Verify(password).Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var repository = new UsersReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();
        

        return new DoLoginUseCase(repository, passwordEncrypter, tokenGenerator);
    }
}
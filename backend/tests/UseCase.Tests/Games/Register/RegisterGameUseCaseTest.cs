using BacklogClear.Application.UseCases.Games.Register;
using BacklogClear.Communication.Enums;
using BacklogClear.Domain.Entities;
using BacklogClear.Exception.ExceptionBase;
using BacklogClear.Exception.Resources;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Games;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Services.LoggedUser;
using FluentAssertions;

namespace UseCase.Tests.Games.Register;

public class RegisterGameUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var request = RequestRegisterGameJsonBuilder.Build();
        
        var result = await useCase.Execute(request);
        
        result.Should().NotBeNull();
        result.Title.Should().Be(request.Title);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public async Task ErrorTitleEmpty(string title)
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var request = RequestRegisterGameJsonBuilder.Build();
        request.Title = title;
        
        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        
        result.Where(exception => exception.GetErrorMessages().Count() == 1 && 
                                  exception.GetErrorMessages().Contains(ResourceErrorMessages.TITLE_REQUIRED));
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public async Task ErrorGenreEmpty(string genre)
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var request = RequestRegisterGameJsonBuilder.Build();
        request.Genre = genre;
        
        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        
        result.Where(exception => exception.GetErrorMessages().Count() == 1 && 
                                  exception.GetErrorMessages().Contains(ResourceErrorMessages.GENRE_REQUIRED));
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public async Task ErrorPlatformEmpty(string platform)
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var request = RequestRegisterGameJsonBuilder.Build();
        request.Platform = platform;
        
        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        
        result.Where(exception => exception.GetErrorMessages().Count() == 1 && 
                                  exception.GetErrorMessages().Contains(ResourceErrorMessages.PLATFORM_REQUIRED));
    }
    
    [Fact]
    public async Task ErrorReleaseDateInvalid()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var request = RequestRegisterGameJsonBuilder.Build();
        request.ReleaseDate = DateTime.UtcNow.AddDays(1);
        
        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        
        result.Where(exception => exception.GetErrorMessages().Count() == 1 && 
                                  exception.GetErrorMessages().Contains(ResourceErrorMessages.RELEASE_DATE_MUST_BE_IN_PAST));
    }
    
    [Fact]
    public async Task ErrorStatusInvalid()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var request = RequestRegisterGameJsonBuilder.Build();
        request.Status = (Status)700;
        
        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        
        result.Where(exception => exception.GetErrorMessages().Count() == 1 && 
                                  exception.GetErrorMessages().Contains(ResourceErrorMessages.STATUS_INVALID));
    }
    
    [Fact]
    public async Task ErrorStartPlayingDateAndEndDateInvalid()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var request = RequestRegisterGameJsonBuilder.Build();
        request.Status = Status.Backlog;
        request.StartPlayingDate = DateTime.UtcNow;
        request.EndPlayingDate = DateTime.UtcNow;
        
        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        
        result.Where(exception => exception.GetErrorMessages().Count() == 2 && 
                                  exception.GetErrorMessages().Contains(ResourceErrorMessages.END_DATE_NOT_ALLOWED) &&
                                  exception.GetErrorMessages().Contains(ResourceErrorMessages.START_DATE_NOT_ALLOWED));
    }

    private RegisterGameUseCase CreateUseCase(User user)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var repository = GamesWriteOnlyRepositoryBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        
        return new RegisterGameUseCase(repository, unitOfWork, mapper, loggedUser);
    }
}
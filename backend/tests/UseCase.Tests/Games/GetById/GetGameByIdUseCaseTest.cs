using BacklogClear.Application.UseCases.Games.GetById;
using BacklogClear.Domain.Entities;
using BacklogClear.Exception.ExceptionBase;
using BacklogClear.Exception.Resources;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories.Games;
using CommonTestUtilities.Services.LoggedUser;
using FluentAssertions;

namespace UseCase.Tests.Games.GetById;

public class GetGameByIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var game = GameBuilder.Build(user);
        var useCase = CreateUseCase(user, game);
        
        var result = await useCase.Execute(game.Id);
        
        result.Should().NotBeNull();
        result.Id.Should().Be(game.Id);
        result.Title.Should().Be(game.Title);
    }

    [Fact]
    public async Task ErrorGameNotFound()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        
        var act = async () => await useCase.Execute(1);
        
        var result = await act.Should().ThrowAsync<NotFoundException>();
        result.Where(exception => exception.GetErrorMessages().Count == 1 && exception.GetErrorMessages().Contains(ResourceErrorMessages.GAME_NOT_FOUND));
    }
    
    private GetGameByIdUseCase CreateUseCase(User user, Game? game = null)
    {
        var mappper = MapperBuilder.Build();
        var repository = new GamesReadOnlyRepositoryBuilder().GetById(game, user).Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        
        return new GetGameByIdUseCase(repository, mappper, loggedUser);
    }
}
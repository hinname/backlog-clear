using BacklogClear.Application.UseCases.Games.GetAll;
using BacklogClear.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories.Games;
using CommonTestUtilities.Services.LoggedUser;
using FluentAssertions;

namespace UseCase.Tests.Games.GetAll;

public class GetAllGamesUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var games = GameBuilder.Collection(user);
        var useCase = CreateUseCase(games, user);
        var result = await useCase.Execute();
        
        result.Should().NotBeNull();
        result.Games.Should().NotBeNull();
        result.Games.Count.Should().Be(3);
    }

    private static GetAllGamesUseCase CreateUseCase(List<Game> games, User user)
    {
        var mapper = MapperBuilder.Build();
        var repository = new GamesReadOnlyRepositoryBuilder().GetAll(games, user).Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        
        return new GetAllGamesUseCase(repository, mapper, loggedUser);
    }
}   
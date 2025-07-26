using BacklogClear.Application.UseCases.Games.GetAll;
using BacklogClear.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories.Games;
using CommonTestUtilities.Services.LoggedUser;
using FluentAssertions;
using BacklogClear.Communication.Enums;

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
        result.Games.Should().NotBeNull().And.AllSatisfy(game =>
        {
            game.Id.Should().BeGreaterThan(0);
            game.Title.Should().NotBeNullOrWhiteSpace();
            game.Platform.Should().NotBeNullOrWhiteSpace();
            game.Status.Should().BeOneOf(Status.Backlog, Status.Playing, Status.Completed, Status.Dropped);
        });
    }

    private static GetAllGamesUseCase CreateUseCase(List<Game> games, User user)
    {
        var mapper = MapperBuilder.Build();
        var repository = new GamesReadOnlyRepositoryBuilder().GetAll(games, user).Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        
        return new GetAllGamesUseCase(repository, mapper, loggedUser);
    }
}   
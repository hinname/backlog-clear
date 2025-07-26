using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Repositories.Games;
using Moq;

namespace CommonTestUtilities.Repositories.Games;

public class GamesReadOnlyRepositoryBuilder
{
    private readonly Mock<IGamesReadOnlyRepository> _repository;

    public GamesReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IGamesReadOnlyRepository>();
    }
    
    public GamesReadOnlyRepositoryBuilder GetAll(List<Game> games, User user)
    {
        _repository.Setup(repo => repo.GetAll(user)).ReturnsAsync(games);
        return this;
    }
    
    public IGamesReadOnlyRepository Build() => _repository.Object;
}

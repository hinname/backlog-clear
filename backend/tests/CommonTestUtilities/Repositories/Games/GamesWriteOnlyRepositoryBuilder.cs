using BacklogClear.Domain.Repositories.Games;
using Moq;

namespace CommonTestUtilities.Repositories.Games;

public class GamesWriteOnlyRepositoryBuilder
{
    public static IGamesWriteOnlyRepository Build()
    {
        var mock = new Mock<IGamesWriteOnlyRepository>();
        return mock.Object;
    }
}
using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Repositories.Games;

namespace BacklogClear.Infrastructure.DataAccess.Repositories;

internal class GamesRepository: IGamesRepository
{
    public void Add(Game game)
    {
        var dbContext = new BacklogClearDbContext();
        dbContext.Games.Add(game);
        dbContext.SaveChanges();
    }
}
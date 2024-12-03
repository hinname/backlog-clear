using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Repositories.Games;

namespace BacklogClear.Infrastructure.DataAccess.Repositories;

internal class GamesRepository: IGamesRepository
{
    private readonly BacklogClearDbContext _dbContext;
    public GamesRepository(BacklogClearDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Game game)
    {
        await _dbContext.games.AddAsync(game);
    }
}
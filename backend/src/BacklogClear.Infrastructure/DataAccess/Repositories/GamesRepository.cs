using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Repositories.Games;
using Microsoft.EntityFrameworkCore;

namespace BacklogClear.Infrastructure.DataAccess.Repositories;

internal class GamesRepository: IGamesReadOnlyRepository, IGamesWriteOnlyRepository, IGamesDeleteOnlyRepository
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
    public async Task<List<Game>> GetAll()
    {
        return await _dbContext.games.AsNoTracking().ToListAsync();
    }
    
    public async Task<Game?> GetById(long id)
    {
        return await _dbContext.games.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<bool> Delete(long id)
    {
        var game = await _dbContext.games.FirstOrDefaultAsync(x => x.Id == id);
        if (game == null)
        {
            return false;
        }
        _dbContext.games.Remove(game);
        return true;
    }
}
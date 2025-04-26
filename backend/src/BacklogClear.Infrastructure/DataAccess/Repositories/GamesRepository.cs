using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Repositories.Games;
using Microsoft.EntityFrameworkCore;

namespace BacklogClear.Infrastructure.DataAccess.Repositories;

internal class GamesRepository: IGamesReadOnlyRepository, IGamesWriteOnlyRepository, IGamesDeleteOnlyRepository, IGamesUpdateOnlyRepository
{
    private readonly BacklogClearDbContext _dbContext;
    private IGamesUpdateOnlyRepository _gamesUpdateOnlyRepositoryImplementation;
    public GamesRepository(BacklogClearDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Game game)
    {
        await _dbContext.Games.AddAsync(game);
    }
    public async Task<List<Game>> GetAll()
    {
        return await _dbContext.Games.AsNoTracking().ToListAsync();
    }
    
    async Task<Game?> IGamesReadOnlyRepository.GetById(long id)
    {
        return await _dbContext.Games.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
    
    async Task<Game?> IGamesUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Games.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<bool> Delete(long id)
    {
        var game = await _dbContext.Games.FirstOrDefaultAsync(x => x.Id == id);
        if (game == null)
        {
            return false;
        }
        _dbContext.Games.Remove(game);
        return true;
    }
    public void Update(Game game)
    {
        _dbContext.Games.Update(game);
    }
    
    // public async Task<List<Game>> FilterByReleaseDate(DateOnly releaseDate)
    // {
    //     var startDate = new DateTime(year: releaseDate.Year, month: releaseDate.Month, day: 1).Date;
    //     
    //     var daysInMonth = DateTime.DaysInMonth(releaseDate.Year, releaseDate.Month);
    //     var endDate = new DateTime(
    //         year: releaseDate.Year, 
    //         month: releaseDate.Month, 
    //         day: daysInMonth,
    //         hour: 23,
    //         minute: 59,
    //         second: 59);
    //     return await _dbContext.games
    //         .AsNoTracking()
    //         .Where(game => game.ReleaseDate >= startDate && game.ReleaseDate <= endDate)
    //         .OrderBy(game => game.ReleaseDate)
    //         .ThenBy(game => game.Title)
    //         .ToListAsync();
    // }
    public async Task<List<Game>> FilterByStartPlayingDate(DateTime initialStartDate, DateTime finalStartDate)
    {
        return await _dbContext.Games
            .AsNoTracking()
            .Where(game => game.StartPlayingDate >= initialStartDate && game.StartPlayingDate <= finalStartDate)
            .OrderBy(game => game.StartPlayingDate)
            .ThenBy(game => game.Title)
            .ToListAsync();
    }

}
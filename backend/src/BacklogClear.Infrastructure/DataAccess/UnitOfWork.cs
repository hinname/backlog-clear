using BacklogClear.Domain.Repositories;

namespace BacklogClear.Infrastructure.DataAccess;

internal class UnitOfWork: IUnitOfWork
{
    private readonly BacklogClearDbContext _dbContext;
    public UnitOfWork(BacklogClearDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}
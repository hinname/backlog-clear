using BacklogClear.Domain.Repositories;

namespace BacklogClear.Infrastructure.DataAccess;

internal class UnitOfWork: IUnitOfWork
{
    private readonly BacklogClearDbContext _dbContext;
    public UnitOfWork(BacklogClearDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Commit()
    {
        _dbContext.SaveChanges();
    }
}
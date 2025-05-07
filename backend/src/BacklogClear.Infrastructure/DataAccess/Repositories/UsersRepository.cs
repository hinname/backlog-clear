using BacklogClear.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace BacklogClear.Infrastructure.DataAccess.Repositories;

internal class UsersRepository : IUserReadOnlyRepository
{
    private readonly BacklogClearDbContext _dbContext;
    
    public UsersRepository(BacklogClearDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users
            .AnyAsync(user => user.Email.Equals(email));
    }
}
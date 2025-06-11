using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace BacklogClear.Infrastructure.DataAccess.Repositories;

internal class UsersRepository : IUsersReadOnlyRepository, IUsersWriteOnlyRepository
{
    private readonly BacklogClearDbContext _dbContext;
    
    public UsersRepository(BacklogClearDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }
    
    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users
            .AnyAsync(user => user.Email.Equals(email));
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }
}
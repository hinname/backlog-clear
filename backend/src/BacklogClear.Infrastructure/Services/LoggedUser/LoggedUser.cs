using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Security.Tokens;
using BacklogClear.Domain.Services.LoggedUser;
using BacklogClear.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BacklogClear.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly BacklogClearDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(BacklogClearDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }
    
    public async Task<User> Get()
    {
        string token = _tokenProvider.TokenOnRequest();
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
        var identifier =jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;
        
        return await _dbContext.Users
            .AsNoTracking()
            .FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
    }
}
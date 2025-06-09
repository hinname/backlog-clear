using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace BacklogClear.Infrastructure.Security.Tokens;

internal class JwtTokenGenerator : IAccessTokenGenerator
{
    private readonly uint _expirationInMinutes;
    private readonly string _signingKey;

    public JwtTokenGenerator(uint expirationInMinutes, string signingKey)
    {
        _expirationInMinutes = expirationInMinutes;
        _signingKey = signingKey;
    }
    
    public string Generate(User user)
    {
        
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Sid, user.UserIdentifier.ToString())
        };
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expirationInMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey)),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
}
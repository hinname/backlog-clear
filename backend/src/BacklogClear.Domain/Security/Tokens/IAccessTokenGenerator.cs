using BacklogClear.Domain.Entities;

namespace BacklogClear.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}
using BacklogClear.Domain.Entities;

namespace BacklogClear.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    Task<User> Get();
}
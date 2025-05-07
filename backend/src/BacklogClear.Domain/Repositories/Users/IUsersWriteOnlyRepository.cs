using BacklogClear.Domain.Entities;

namespace BacklogClear.Domain.Repositories.Users;

public interface IUsersWriteOnlyRepository
{
    Task Add(User user);
}
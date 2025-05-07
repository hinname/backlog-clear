namespace BacklogClear.Domain.Repositories.Users;

public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWithEmail(string email);
}
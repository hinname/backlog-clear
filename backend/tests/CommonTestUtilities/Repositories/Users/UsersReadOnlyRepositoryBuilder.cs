using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories.Users;

public class UsersReadOnlyRepositoryBuilder
{
    private readonly Mock<IUsersReadOnlyRepository> _repository;

    public UsersReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IUsersReadOnlyRepository>();
    }
    
    public UsersReadOnlyRepositoryBuilder ExistActiveUserWithEmail(string? email = null)
    {
        if (!string.IsNullOrWhiteSpace(email))
        {
            _repository.Setup(repo => repo.ExistActiveUserWithEmail(email))
                .ReturnsAsync(true);
        }
        
        return this;
    }
    
    public UsersReadOnlyRepositoryBuilder GetUserByEmail(User user)
    {
        _repository.Setup(repo => repo.GetUserByEmail(user.Email))
            .ReturnsAsync(user);

        return this;
    }
    
    public IUsersReadOnlyRepository Build() => _repository.Object;
}
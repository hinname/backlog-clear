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
    
    public IUsersReadOnlyRepository Build() => _repository.Object;
}
using BacklogClear.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories.Users;

public class UsersWriteOnlyRepositoryBuilder
{
    public static IUsersWriteOnlyRepository Build()
    {
        var mock = new Mock<IUsersWriteOnlyRepository>();
        
        return mock.Object;
    }
}
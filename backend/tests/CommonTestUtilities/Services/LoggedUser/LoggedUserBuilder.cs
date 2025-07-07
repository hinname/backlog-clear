using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Services.LoggedUser;
using Moq;

namespace CommonTestUtilities.Services.LoggedUser;

public class LoggedUserBuilder
{
    public static ILoggedUser Build(User user)
    {
        var mock = new Mock<ILoggedUser>();
        mock.Setup(loggedU => loggedU.Get()).ReturnsAsync(user);
        return mock.Object;
    }
}
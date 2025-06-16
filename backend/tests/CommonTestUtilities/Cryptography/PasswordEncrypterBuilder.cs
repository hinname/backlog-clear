using BacklogClear.Domain.Security.Crytography;
using Moq;

namespace CommonTestUtilities.Cryptography;

public class PasswordEncrypterBuilder
{
    public static IPasswordEncrypter Build()
    {
        var mock = new Mock<IPasswordEncrypter>();
        
        mock.Setup(passwordEncripter => passwordEncripter.Encrypt(It.IsAny<string>()))
            .Returns("hashedPassword");
        
        return mock.Object;
    }
}
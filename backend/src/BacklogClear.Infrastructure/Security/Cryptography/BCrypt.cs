using BacklogClear.Domain.Security.Crytography;
using BCryptNet = BCrypt.Net.BCrypt;

namespace BacklogClear.Infrastructure.Security.Cryptography;

internal class BCrypt: IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        return BCryptNet.HashPassword(password);
    }
    
    public bool Verify(string password, string hashedPassword)
    {
        return BCryptNet.Verify(password, hashedPassword);
    }
}
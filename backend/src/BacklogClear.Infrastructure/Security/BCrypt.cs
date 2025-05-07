using BacklogClear.Domain.Security.Crytography;
using BCryptNet = BCrypt.Net.BCrypt;

namespace BacklogClear.Infrastructure.Security;

internal class BCrypt: IPasswordEncripter
{
    public string Encrypt(string password)
    {
        return BCryptNet.HashPassword(password);
    }
}
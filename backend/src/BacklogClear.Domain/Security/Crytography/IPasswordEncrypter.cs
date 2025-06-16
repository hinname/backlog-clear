namespace BacklogClear.Domain.Security.Crytography;

public interface IPasswordEncrypter
{
    string Encrypt(string password);
    bool Verify(string password, string hashedPassword);
}
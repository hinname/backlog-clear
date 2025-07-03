namespace BacklogClear.Domain.Security.Tokens;

public interface ITokenProvider
{
    string TokenOnRequest();
}
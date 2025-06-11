using System.Net;
using BacklogClear.Exception.Resources;

namespace BacklogClear.Exception.ExceptionBase;

public class InvalidLoginException : BacklogClearException
{
    public InvalidLoginException() : base(ResourceErrorMessages.LOGIN_EMAIL_OR_PASSWORD_INVALID)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrorMessages()
    {
        return [Message];
    }
}
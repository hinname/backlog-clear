using System.Net;

namespace BacklogClear.Exception.ExceptionBase;

public class ErrorOnValidationException : BacklogClearException
{
    private readonly List<string> _errorMessages;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrorOnValidationException(List<string> errorMessages): base(string.Empty)
    {
        _errorMessages = errorMessages;
    }

    public override List<string> GetErrorMessages()
    {
        return _errorMessages;
    }
}
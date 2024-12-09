using System.Net;

namespace BacklogClear.Exception.ExceptionBase;

public class NotFoundException : BacklogClearException
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;
    public NotFoundException(string message) : base(message)
    {
        
    }

    public override List<string> GetErrorMessages()
    {
        return [Message];
    }
}
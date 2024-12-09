namespace BacklogClear.Exception.ExceptionBase;

public abstract class BacklogClearException : SystemException
{
    protected BacklogClearException(string message) : base(message)
    {
        
    }
    
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrorMessages();
}
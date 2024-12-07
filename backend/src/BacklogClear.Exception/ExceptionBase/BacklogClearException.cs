namespace BacklogClear.Exception.ExceptionBase;

public abstract class BacklogClearException : SystemException
{
    protected BacklogClearException(string message) : base(message)
    {
        
    }
}
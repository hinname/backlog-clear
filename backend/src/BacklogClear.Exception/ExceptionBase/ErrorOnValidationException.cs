namespace BacklogClear.Exception.ExceptionBase;

public class ErrorOnValidationException : BacklogClearException
{
    public List<string> ErrorMessages { get; set; }

    public ErrorOnValidationException(List<string> errorMessages): base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }
}
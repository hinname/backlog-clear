namespace BacklogClear.Communication.Responses;

public class ResponseErrorJson
{
    public List<string> ErrorMessages { get; set; }
    
    public ResponseErrorJson(string errorMessage)
    {
        ErrorMessages = [errorMessage];
    }

    public ResponseErrorJson(IEnumerable<string> errorMessage)
    {
        ErrorMessages = errorMessage.ToList();
    }
}
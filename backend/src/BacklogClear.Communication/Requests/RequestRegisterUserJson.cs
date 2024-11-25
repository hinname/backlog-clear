namespace BacklogClear.Communication.Requests;

public class RequestRegisterUserJson
{
    public string Email { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
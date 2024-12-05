using BacklogClear.Communication.Enums;

namespace BacklogClear.Communication.Responses.Games;

public class ResponseShortGameJson
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public Status Status { get; set; }
}
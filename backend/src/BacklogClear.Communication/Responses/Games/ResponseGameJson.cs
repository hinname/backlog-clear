using BacklogClear.Communication.Enums;

namespace BacklogClear.Communication.Responses.Games;

public class ResponseGameJson
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public Status Status { get; set; }
}
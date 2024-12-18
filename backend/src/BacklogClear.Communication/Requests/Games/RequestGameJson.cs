using BacklogClear.Communication.Enums;

namespace BacklogClear.Communication.Requests.Games;

public class RequestGameJson
{
    public string Title { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public Status Status { get; set; }
    public DateTime? StartPlayingDate { get; set; }
    public DateTime? EndPlayingDate { get; set; }
}
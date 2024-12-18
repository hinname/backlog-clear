using BacklogClear.Domain.Enums;

namespace BacklogClear.Domain.Entities;

public class Game
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public Status Status { get; set; }
    public DateTime? StartPlayingDate { get; set; }
    public DateTime? EndPlayingDate { get; set; }
}
namespace SMovie.Domain.Models;

public class EpisodeDTO
{
    public Guid EpisodeId { get; set; }
    public int EpisodeNumber { get; set; }
    public string Name { get; set; } = null!;
    public string Video { get; set; } = null!;
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}

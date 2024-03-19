namespace SMovie.Domain.Models;

public class NewCast
{
    public Guid PersonId { get; set; }
    public string CharacterName { get; set; } = null!;
}

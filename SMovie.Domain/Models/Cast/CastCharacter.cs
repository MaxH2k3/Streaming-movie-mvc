namespace SMovie.Domain.Models
{
    public class CastCharacter
    {
        public Guid PersonId { get; set; }
        public string NamePerson { get; set; } = null!;
        public string CharacterName { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
    }
}

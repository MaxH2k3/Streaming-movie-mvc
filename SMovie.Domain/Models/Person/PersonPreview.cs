namespace SMovie.Domain.Models.Person
{
    public class PersonPreview
    {
        public Guid PersonId { get; set; }
        public string Thumbnail { get; set; } = null!;
        public string NamePerson { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}

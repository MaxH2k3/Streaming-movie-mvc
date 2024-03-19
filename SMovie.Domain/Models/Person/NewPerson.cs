using System.ComponentModel.DataAnnotations;

namespace SMovie.Domain.Models
{
    public class NewPerson
    {
        public Guid PersonId { get; set; }
        public string Thumbnail { get; set; } = null!;
        [Required]
        public string NamePerson { get; set; } = null!;
        [Required]
        public string NationId { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;
        public string DoB { get; set; } = null!;
    }
}

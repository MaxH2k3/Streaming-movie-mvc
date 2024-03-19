using System.ComponentModel.DataAnnotations;

namespace SMovie.Domain.Models
{
    public class NewSeason
    {
        [Required]
        public Guid MovieId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}

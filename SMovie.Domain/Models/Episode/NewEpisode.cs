using System.ComponentModel.DataAnnotations;

namespace SMovie.Domain.Models
{
    public class NewEpisode
    {
        public string Name { get; set; } = null!;
        [Required]
        public string Video { get; set; } = null!;
        public DateTime DateCreated { get; set; }
    }
}

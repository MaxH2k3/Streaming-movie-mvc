using SMovie.Domain.Entity;

namespace SMovie.Domain.Models
{
    public class InfoMovie
    {
        public Guid MovieId { get; set; }
        public string  FeatureName { get; set; } = null!;
        public string NationName { get; set; } = null!;
        public string EnglishName { get; set; } = null!;
        public string VietnamName { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime? DateDeleted { get; set; }
        public DateTime ProducedDate { get; set; }
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
    }
}

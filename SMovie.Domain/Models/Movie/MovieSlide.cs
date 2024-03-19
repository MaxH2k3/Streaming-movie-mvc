using SMovie.Domain.Entity;

namespace SMovie.Domain.Models
{
    public class MovieSlide
    {
        public Guid MovieId { get; set; }
        public double Mark { get; set; }
        public int Time { get; set; }
        public string EnglishName { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public virtual FeatureFilm Feature { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime ProducedDate { get; set; }
        public virtual ICollection<Category> Categories { get; set; } = null!;
    }
}

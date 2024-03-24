using SMovie.Domain.Entity;

namespace SMovie.Domain.Models
{
    public class MovieDetail
    {
        public Guid MovieId { get; set; }
        public double Mark { get; set; }
        public int Time { get; set; }
        public int Viewer { get; set; }
        public string Description { get; set; } = null!;
        public string EnglishName { get; set; } = null!;
        public string VietnamName { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string Trailer { get; set; } = null!;
        public DateTime ProducedDate { get; set; }
        public virtual Nation Nation { get; set; } = null!;
        public virtual FeatureFilm Feature { get; set; } = null!;
        public int TotalSeasons { get; set; }
        public int TotalEpisodes { get; set; }
        public string Status { get; set; } = null!;
        public virtual ICollection<SeasonDTO> Seasons { get; set; } = null!;
        public virtual ICollection<CastCharacter> CastCharacteries { get; set; } = null!;
        public virtual ICollection<Category> Categories { get; set; } = null!;
    }
}

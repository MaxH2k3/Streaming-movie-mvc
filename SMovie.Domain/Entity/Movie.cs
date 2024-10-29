namespace SMovie.Domain.Entity
{
    public class Movie
    {
        public Movie()
        {
            Casts = new HashSet<Cast>();
            Seasons = new HashSet<Season>();
            MovieCategories = new HashSet<MovieCategory>();
        }

        public Guid MovieId { get; set; }
        public int FeatureId { get; set; }
        public string NationId { get; set; } = null!;
        public double Mark { get; set; }
        public int Time { get; set; }
        public int Viewer { get; set; }
        public string Description { get; set; } = null!;
        public string EnglishName { get; set; } = null!;
        public string VietnamName { get; set; } = null!;
        public string? Thumbnail { get; set; }
        public string? Trailer { get; set; }
        public string Status { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime ProducedDate { get; set; }
        public virtual FeatureFilm Feature { get; set; } = null!;
        public virtual Nation Nation { get; set; } = null!;
        public virtual ICollection<Cast> Casts { get; set; }
        public virtual ICollection<Season> Seasons { get; set; }
        public virtual ICollection<MovieCategory> MovieCategories { get; set; }
    }
}

namespace SMovie.Domain.Entity
{
    public partial class Episode
    {

        public Guid EpisodeId { get; set; }
        public Guid SeasonId { get; set; }
        public int EpisodeNumber { get; set; }
        public string Name { get; set; } = null!;
        public string Video { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public virtual Season Season { get; set; } = null!;
    }
}

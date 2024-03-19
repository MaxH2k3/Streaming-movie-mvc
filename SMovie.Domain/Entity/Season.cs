namespace SMovie.Domain.Entity
{
    public partial class Season
    {
        public Season()
        {
            Episodes = new HashSet<Episode>();
        }

        public Guid SeasonId { get; set; }
        public Guid MovieId { get; set; }
        public int SeasonNumber { get; set; }
        public string Name { get; set; } = null!;
        public string Status { get; set; } = null!;
        public virtual Movie Movie { get; set; } = null!;
        public virtual ICollection<Episode> Episodes { get; set; }
    }
}

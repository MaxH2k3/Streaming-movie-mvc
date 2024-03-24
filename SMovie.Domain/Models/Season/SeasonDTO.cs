namespace SMovie.Domain.Models
{
    public class SeasonDTO
    {
        public Guid SeasonId { get; set; }
        public int SeasonNumber { get; set; }
        public string Name { get; set; } = null!;
        public virtual ICollection<EpisodeDTO> Episodes { get; set; } = new List<EpisodeDTO>();
    }
}

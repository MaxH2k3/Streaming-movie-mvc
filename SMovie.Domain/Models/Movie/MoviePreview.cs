namespace SMovie.Domain.Models
{
    public class MoviePreview
    {
        public Guid MovieId { get; set; }
        public string VietnamName { get; set; } = null!;
        public string EnglishName { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public int Time { get; set; }
        
    }
}

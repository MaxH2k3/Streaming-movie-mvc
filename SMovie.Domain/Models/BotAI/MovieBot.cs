namespace SMovie.Domain.Models
{
    public class MovieBot
    {
        public string Authorization { get; set; } = null!;
        public string Connection { get; set; } = null!;
        public string BotId { get; set; } = null!;
        public bool Stream { get; set; }
        public string User { get; set; } = null!;
    }
}

using SMovie.Domain.Models;

namespace SMovie.Dashboard.Models
{
    public class DashboardData
    {
        public IEnumerable<NumofMovieCategory> ListMovieCategory { get; set; } = new List<NumofMovieCategory>();
        public Dictionary<MoviePreview, int> ListMovieTop { get; set; } = new Dictionary<MoviePreview, int>();
        public Dictionary<string, int> ListMovieOnPage { get; set; } = new Dictionary<string, int>();
    }
}

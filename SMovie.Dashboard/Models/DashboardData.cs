using SMovie.Domain.Models;

namespace SMovie.Dashboard.Models
{
    public class DashboardData
    {
        public IEnumerable<NumofMovieCategory> listMovieCategory { get; set; } = new List<NumofMovieCategory>();
    }
}

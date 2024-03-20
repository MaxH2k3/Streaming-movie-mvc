using SMovie.Domain.Models;

namespace SMovie.WebUI.Models
{
    public class HomeModel
    {
        public IEnumerable<MovieSlide> SlideMovies { get; set; } = null!;
        public IEnumerable<MoviePreview> NewMovies { get; set; } = null!;
    }
}

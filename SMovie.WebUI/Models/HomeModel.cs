using SMovie.Domain.Models;

namespace SMovie.WebUI.Models
{
    public class HomeModel
    {
        // Get movies for slide
        public IEnumerable<MovieSlide> SlideMovies { get; set; } = null!;

        // Get newest movies
        public IEnumerable<MoviePreview> NewMovies { get; set; } = null!;

        // Get movies upcoming
        public IEnumerable<MoviePreview> UpcomingMovies { get; set; } = null!;

        // Get top 10 movies most viewed
        public IEnumerable<MoviePreview> TopViewedMovies { get; set; } = null!;

        // Get top 10 movies most rating
        public IEnumerable<MoviePreview> TopRatingMovies { get; set; } = null!;

        // Get top 10 TV Series newest
        public IEnumerable<MovieDetail> NewTVSeries { get; set; } = null!;

        // Get top 10 stand alone movies newest
        public IEnumerable<MovieSlide> NewStandaloneMovies { get; set; } = null!;
    }
}

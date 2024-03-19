namespace SMovie.Domain.Entity
{
    public class MovieCategory
    {
        public int CategoryId { get; set; }
        public Guid MovieId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Movie Movie { get; set; } = null!;

        public MovieCategory()
        {
        }

        public MovieCategory(int CategoryId, Guid MovieId)
        {
            this.CategoryId = CategoryId;
            this.MovieId = MovieId;
        }
    }
}

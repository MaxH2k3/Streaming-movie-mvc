using SMovie.Domain.Entity;

namespace SMovie.Dashboard.Models
{
    public class CreateModelMovie
    {
        public IEnumerable<Nation> Nations { get; set; } = new List<Nation>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}

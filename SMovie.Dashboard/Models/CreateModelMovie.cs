using SMovie.Domain.Entity;
using SMovie.Domain.Models;

namespace SMovie.Dashboard.Models
{
    public class CreateModelMovie
    {
        public IEnumerable<Nation> Nations { get; set; } = new List<Nation>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<PersonPreview> Persons { get; set; } = new List<PersonPreview>();
    }
}

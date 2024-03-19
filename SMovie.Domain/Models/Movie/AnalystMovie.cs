using MongoDB.Bson;

namespace SMovie.Domain.Models
{
    public class AnalystMovie
    {
        public ObjectId Id { get; set; }
        public Guid MovieId { get; set; }
        public int Viewer { get; set; }
    }
}

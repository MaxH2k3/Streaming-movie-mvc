using MongoDB.Bson;

namespace SMovie.Domain.Models
{
    public class Notification
    {
        public ObjectId Id { get; set; }
        public string? TypeMessage { get; set; }
        public string Message { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ExpiredDate { get; set; }
    }
}

using MongoDB.Bson;
using SMovie.Domain.Enum;

namespace SMovie.Domain.Models
{
    public class Notification
    {
        public ObjectId Id { get; set; }
        public MethodType TypeMessage { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid? UserId { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ExpiredDate { get; set; }
    }
}

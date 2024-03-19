using MongoDB.Bson;

namespace SMovie.Domain.Models
{
    public class VerifyToken
    {
        public ObjectId Id { get; set; }
        public Guid MID { get; set; }
        public string Token { get; set; } = null!;
        public int Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}

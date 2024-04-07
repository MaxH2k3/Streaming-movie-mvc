using MongoDB.Bson;
using System.Net;

namespace SMovie.Domain.Models
{
    public class BlackIP
    {
        public ObjectId Id { get; set; }
        public IPAddress IP { get; set; } = null!;
        public string? Note { get; set; }
        public string Status { get; set; } = null!;
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}

namespace SMovie.Domain.Models
{
    public class UserMail
    {
        public string UserName { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Code { get; set; }
    }
}

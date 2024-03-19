namespace SMovie.Domain.Entity
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public DateTime? DateCreated { get; set; }
    }
}

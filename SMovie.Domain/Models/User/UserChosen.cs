namespace SMovie.Domain.Models
{
    public class UserChosen
    {
        public Guid UserId { get; set; }
        public string DisplayName { get; set; } = null!;
        public string Avatar { get; set; } = null!;
    }
}

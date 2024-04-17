namespace SMovie.Domain.Models
{
    public class CookieSetting
    {
        public bool HttpOnly { get; set; }
        public int SecurePolicy { get; set; }
        public int SameSite { get; set; }
        public string Name { get; set; } = null!;
        public bool AllowRefresh { get; set; }
        public bool IsPersistent { get; set; }
        public int ExpireTime { get; set; }
    }
}

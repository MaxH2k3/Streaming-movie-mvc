namespace SMovie.Domain.Models
{
    public class CookieSetting
    {
        public string LoginPath { get; set; } = null!;
        public string LogoutPath { get; set; } = null!;
        public string AccessDeniedPath { get; set; } = null!;
        public bool HttpOnly { get; set; }
        public int SecurePolicy { get; set; }
        public int SameSite { get; set; }
        public string Name { get; set; } = null!;
        public bool AllowRefresh { get; set; }
        public bool IsPersistent { get; set; }
        public int ExpireTime { get; set; }
    }
}

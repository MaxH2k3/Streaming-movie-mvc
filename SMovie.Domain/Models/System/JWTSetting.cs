using Microsoft.Extensions.Configuration;

namespace SMovie.Domain.Models
{
    public class JWTSetting
    {
        public string SecurityKey { get; set; } = null!;
        public double TokenExpiry { get; set; }
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;

        public JWTSetting()
        {
            GetSettingConfig();
        }

        private void GetSettingConfig()
        {
            IConfiguration config = new ConfigurationBuilder()

            .SetBasePath(Directory.GetCurrentDirectory())

            .AddJsonFile("appsettings.json", true, true)

            .Build();

            this.SecurityKey = config["JWTSetting:Securitykey"]!;
            this.Issuer = config["JWTSetting:Issuer"]!;
            this.Audience = config["JWTSetting:Audience"]!;
            this.TokenExpiry = Convert.ToDouble(config["JWTSetting:TokenExpiry"]);

        }
    }
}

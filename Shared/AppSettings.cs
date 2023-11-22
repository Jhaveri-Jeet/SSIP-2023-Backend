namespace CriminalDatabaseBackend.Shared
{
    public class AppSettings
    {
        public string Host { get; set; } = null!;
        public string Protocol { get; set; } = null!;
        public string JWTIssuer { get; set; } = null!;
        public string JWTSecurityKey { get; set; } = null!;
    }
}

namespace ArchimeManagement.Models
{
    public class AppSettings
    {
        private static volatile AppSettings _instance;
        private static object lockObject = new object();
        public static AppSettings Build()
        {
            if (_instance is null)
            {
                lock (lockObject)
                {
                    if (_instance == null)
                        _instance = GetConfiguration().Get<AppSettings>();
                }
            }
            return _instance;
        }

        public required string IdentityServerAddress { get; set; }
        public required string[] CORSTrustedOrigins { get; set; }

        private static IConfiguration GetConfiguration()
        {
#if RELEASE
                IConfiguration _configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Production.json")
                    .Build();
#else
            IConfiguration _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();
#endif

            return _configuration;
        }
    }
}

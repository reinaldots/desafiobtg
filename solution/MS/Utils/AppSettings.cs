using Microsoft.Extensions.Configuration;

namespace DesafioBTG.MS.Utils
{
    public static class AppSettings
    {
        private static IConfiguration? _configuration;

        public static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .Build();
                }

                return _configuration;
            }
        }
    }
}

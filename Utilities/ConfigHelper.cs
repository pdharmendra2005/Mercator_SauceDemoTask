using Microsoft.Extensions.Configuration;


namespace Mercator_SauceDemoTask.Utilities
{
    public static class ConfigHelper
    {
        private static IConfiguration _config;

        static ConfigHelper()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _config = builder.Build();
        }

        public static string GetSetting(string key)
        {
            return _config[$"AppSettings:{key}"];
        }

        public static bool GetBoolSetting(string key)
        {
            return bool.Parse(_config[$"AppSettings:{key}"]);
        }
    }
}

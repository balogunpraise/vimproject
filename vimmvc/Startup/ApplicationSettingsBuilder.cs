namespace vimmvc.Startup
{
    public static class ApplicationSettingsBuilder
    {
        public static IHostBuilder ConfigureAppSettings(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration((builder) =>
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if(environment != null)
                {
                    builder.AddJsonFile("appSettings.json", false, true);
                    builder.AddJsonFile($"appSettings.{environment}.json", true, true);
                    builder.AddJsonFile($"appSettings.{Environment.MachineName}.json", true, true);

                    builder.AddEnvironmentVariables();
                }
            });
            return host;
        }
    }
}

namespace ProfilerService.Grpc.Infrastructure.Configurations;

public static partial class AppConfiguration
{
    public static WebApplicationBuilder AddAppSettings(this WebApplicationBuilder applicationBuilder, string prefix = "ProfileService_")
    {
        applicationBuilder.Host.ConfigureAppConfiguration(config =>
        {
            config.AddEnvironmentVariables(prefix);
            config.Build();
        });

        return applicationBuilder;
    }
}

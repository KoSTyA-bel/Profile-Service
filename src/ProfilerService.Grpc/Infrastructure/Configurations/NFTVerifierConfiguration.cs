using ProfilerService.BLL.Settings;

namespace ProfilerService.Grpc.Infrastructure.Configurations;

public static class NFTVerifierConfiguration
{
    public static WebApplicationBuilder AddNFTVerifierSettings(this WebApplicationBuilder applicationBuilder, string prefix = "ProfileService_")
    {
        var section = applicationBuilder.Configuration.GetSection(nameof(NFTVerifierSettings));
        applicationBuilder.Services.Configure<NFTVerifierSettings>(applicationBuilder.Configuration.GetSection("NFTVerifierSettings"));

        return applicationBuilder;
    }

    public static (string, string, string, string, string) AcceptEnvironmentVariablesToNFTVerifierSettings(this WebApplicationBuilder applicationBuilder)
    {
        return (
            applicationBuilder.Configuration.GetValue<string>("ProfileService_ApiUrl"),
            applicationBuilder.Configuration.GetValue<string>("ProfileService_CollectionName"),
            applicationBuilder.Configuration.GetValue<string>("ProfileService_CommonTemplate"),
            applicationBuilder.Configuration.GetValue<string>("ProfileService_RareTemplate"),
            applicationBuilder.Configuration.GetValue<string>("ProfileService_EpicTemplate")
        );
    }
}

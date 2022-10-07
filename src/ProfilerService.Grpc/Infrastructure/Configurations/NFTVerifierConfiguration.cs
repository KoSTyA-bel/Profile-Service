using ProfileService.BLL.Settings;

namespace ProfileService.Grpc.Infrastructure.Configurations;

public static class NFTVerifierConfiguration
{
    public static WebApplicationBuilder AddNFTVerifierSettings(this WebApplicationBuilder applicationBuilder, string prefix = "ProfileService_")
    {
        var section = applicationBuilder.Configuration.GetSection(nameof(NFTVerifierSettings));
        applicationBuilder.Services.Configure<NFTVerifierSettings>(applicationBuilder.Configuration.GetSection("NFTVerifierSettings"));

        return applicationBuilder;
    }
}

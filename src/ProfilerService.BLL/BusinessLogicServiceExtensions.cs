using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProfileService.BLL.BusinessLogic;
using ProfileService.BLL.Interfaces;
using ProfileService.BLL.Providers;
using ProfileService.BLL.Services;
using ProfileService.BLL.Settings;
using ProfileService.BLL.Verifiers;

namespace ProfileService.BLL;

public static class BusinessLogicServiceExtensions
{
    public static IServiceCollection AddWaxWalletVerifier(this IServiceCollection services)
    {
        services.AddSingleton(GetINFTVerifierSettings);

        services.AddScoped<INFTVerifier, NFTVerifier>();
        services.AddScoped<INFTVerifyService, NFTVerifyService>();

        return services;

        static INFTVerifierSettings GetINFTVerifierSettings(IServiceProvider sp)
        {
            var settings = sp.GetRequiredService<IOptions<NFTVerifierSettings>>().Value;
            return settings;
        }
    }

    public static IServiceCollection AddProfileService(this IServiceCollection services)
    {
        services.AddScoped<IProfileService, Services.ProfileService>();
        services.AddSingleton<IBattleResultCounter, Business>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProfilerService.BLL.BusinessLogic;
using ProfilerService.BLL.Interfaces;
using ProfilerService.BLL.Providers;
using ProfilerService.BLL.Services;
using ProfilerService.BLL.Settings;
using ProfilerService.BLL.Verifiers;

namespace ProfilerService.BLL;

public static class BusinessLogicServiceExtensions
{
    public static IServiceCollection AddWaxWalletVerifier(this IServiceCollection services, NFTVerifierSettings settings = null)
    {
        if (settings is null)
        {
            services.AddSingleton<INFTVerifierSettings>(sp => sp.GetRequiredService<IOptions<NFTVerifierSettings>>().Value);
        }
        else
        {
            services.AddSingleton<INFTVerifierSettings>(settings);
        }
        services.AddScoped<INFTVerifier, NFTVerifier>();
        services.AddScoped<INFTVerifyService, NFTVerifyService>();

        return services;
    }

    public static IServiceCollection AddProfileService(this IServiceCollection services)
    {
        services.AddScoped<IProfileService, ProfileService>();
        services.AddSingleton<IDepositer, Business>();
        services.AddSingleton<IWithdrawer, Business>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}

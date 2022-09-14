using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProfilerService.BLL.BusinessLogic;
using ProfilerService.BLL.Interfaces;
using ProfilerService.BLL.Providerc;
using ProfilerService.BLL.Services;
using ProfilerService.BLL.Settings;
using ProfilerService.BLL.Verifiers;

namespace ProfilerService.BLL;

public static class BusinessLogicServiceExtensions
{
    public static IServiceCollection AddWaxWalletVerifier(this IServiceCollection services, WaxWalletVerifierSettings settings = null)
    {
        if (settings is null)
        {
            services.AddSingleton<IWaxWalletVerifierSettings>(sp => sp.GetRequiredService<IOptions<WaxWalletVerifierSettings>>().Value);
        }
        else
        {
            services.AddSingleton<IWaxWalletVerifierSettings>(settings);
        }
        services.AddScoped<IWaxWalletVerifier, WaxWalletVerifier>();
        services.AddScoped<IWaxWalletVerifyService, WaxWalletVerifyService>();

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

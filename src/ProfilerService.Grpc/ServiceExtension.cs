using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProfilerService.BLL.Interfaces;
using ProfilerService.BLL.Services;
using ProfilerService.BLL.Settings;
using ProfilerService.BLL.Verifiers;
using ProfilerService.DLL.Contexts;
using ProfilerService.DLL.Providers;
using ProfilerService.DLL.Repositories;

namespace ProfilerService.Grpc;

public static class ServiceExtension
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
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IProfileProvider, ProfileProvider>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IDataContext, ProfileDataContext>();

        return services;
    }
}

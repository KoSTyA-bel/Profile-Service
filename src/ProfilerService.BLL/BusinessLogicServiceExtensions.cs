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
    public static IServiceCollection AddWaxWalletVerifier(this IServiceCollection services,
        (string apiUrl, string collectionName, string commonTemplate, string rareTemplate, string epicTemplate) enviromentVariables)
    {
        services.AddSingleton<INFTVerifierSettings>(GetINFTVerifierSettings);

        services.AddScoped<INFTVerifier, NFTVerifier>();
        services.AddScoped<INFTVerifyService, NFTVerifyService>();

        return services;

        INFTVerifierSettings GetINFTVerifierSettings(IServiceProvider sp)
        {
            var settings = sp.GetRequiredService<IOptions<NFTVerifierSettings>>().Value;

            settings.ApiUrl = enviromentVariables.apiUrl ?? settings.ApiUrl;
            settings.CollectionName = enviromentVariables.collectionName ?? settings.CollectionName;
            settings.CommonTemplate = enviromentVariables.commonTemplate ?? settings.CommonTemplate;
            settings.RareTemplate = enviromentVariables.rareTemplate ?? settings.RareTemplate;
            settings.EpicTemplate = enviromentVariables.epicTemplate ?? settings.EpicTemplate;

            return settings;
        }
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

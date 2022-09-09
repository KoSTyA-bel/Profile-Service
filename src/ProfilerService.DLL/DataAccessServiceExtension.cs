using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProfilerService.BLL.Interfaces;
using ProfilerService.DLL.Contexts;
using ProfilerService.DLL.Providers;
using ProfilerService.DLL.Repositories;

namespace ProfilerService.DLL;

public static class DataAccessServiceExtension
{
    public static IServiceCollection AddProfileDataBase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextPool<ProfileContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped(provider =>
        {
            var service = provider.GetService(typeof(ProfileContext)) as ProfileContext;
            return service.Profiles;
        });
        services.AddScoped<IDataContext, ProfileDataContext>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IProfileProvider, ProfileProvider>();

        return services;
    }
}

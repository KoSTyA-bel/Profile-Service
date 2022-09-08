using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProfilerService.DLL.Contexts;

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

        return services;
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfileService.DLL.Repositories;
using ProfileService.BLL.Entities;
using ProfileService.BLL.Interfaces;

namespace ProfileService.DLL.Providers;

public class ProfileProvider : IProfileProvider
{
    private readonly DbSet<Profile> _profiles;
    private readonly ILogger<ProfileProvider> _logger;

    public ProfileProvider(DbSet<Profile> profiles, ILogger<ProfileProvider> logger)
    {
        _profiles = profiles ?? throw new ArgumentNullException(nameof(profiles));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<IEnumerable<Profile>> GetAllProfiles(CancellationToken token)
    {
        _logger.LogTrace("Get all profiles");
        return Task.FromResult((IEnumerable<Profile>)_profiles.ToArray());
    }

    public Task<Profile> GetByDiscordId(ulong discordId, CancellationToken token)
    {
        _logger.LogTrace("Get profile with discordId={discordId} to database", discordId);
        return _profiles.FirstOrDefaultAsync(x => x.DiscrodId == discordId);
    }

    public Task<IEnumerable<Profile>> GetProfiles(int page, int pageSize, CancellationToken token)
    {
        _logger.LogTrace("Get profiles pahe={page}, pageSize={pageSize}", page, pageSize);
        return Task.FromResult((IEnumerable<Profile>)_profiles
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToArray());
    }
}

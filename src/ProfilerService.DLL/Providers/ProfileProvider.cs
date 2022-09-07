using Microsoft.EntityFrameworkCore;
using ProfilerService.BLL.Entities;
using ProfilerService.BLL.Interfaces;

namespace ProfilerService.DLL.Providers;

public class ProfileProvider : IProfileProvider
{
    private readonly DbSet<Profile> _profiles;

    public ProfileProvider(DbSet<Profile> profiles)
    {
        _profiles = profiles ?? throw new ArgumentNullException(nameof(profiles));
    }

    public Task<Profile> GetByDiscordId(ulong discordId, CancellationToken token) => _profiles.FirstOrDefaultAsync(x => x.DiscrodId == discordId);

    public Task<IEnumerable<Profile>> GetProfiles(int page, int pageSize, CancellationToken token) => Task.FromResult((IEnumerable<Profile>)_profiles
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToArray());
}

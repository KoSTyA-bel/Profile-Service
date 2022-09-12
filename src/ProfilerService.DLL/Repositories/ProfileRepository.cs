using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfilerService.BLL.Entities;
using ProfilerService.BLL.Interfaces;
using ProfilerService.DLL.Contexts;

namespace ProfilerService.DLL.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly DbSet<Profile> _profiles;
    private readonly ILogger<ProfileRepository> _logger;

    public ProfileRepository(DbSet<Profile> profiles, ILogger<ProfileRepository> logger)
    {
        _profiles = profiles ?? throw new ArgumentNullException(nameof(profiles));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task Create(Profile profile, CancellationToken token)
    {
        _logger.LogTrace("Add new profile with discordId={prfile.DiscordId} to database", profile.DiscrodId);
        _profiles.Add(profile);
        return Task.CompletedTask;
    }

    public Task Delete(Profile profile, CancellationToken token)
    {
        _logger.LogTrace("Delete profile with discordId={profile.DiscrodId}, id={profile.Id} from database", profile.DiscrodId, profile.Id);
        return Task.FromResult(_profiles.Remove(profile));
    }

    public Task Update(Profile profile, CancellationToken token)
    {
        _logger.LogTrace("Update profile with discordId={profile.DiscrodId}, id={profile.Id} from database", profile.DiscrodId, profile.Id);
        return Task.FromResult(_profiles.Update(profile));
    }
}

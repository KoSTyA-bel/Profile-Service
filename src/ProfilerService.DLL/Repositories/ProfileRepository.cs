using Microsoft.EntityFrameworkCore;
using ProfilerService.BLL.Entities;
using ProfilerService.BLL.Interfaces;
using ProfilerService.DLL.Contexts;

namespace ProfilerService.DLL.Repositories;

public class ProfileRepository : IProfileRepository
{
    // todo: inject from di DbSet<>
    private readonly ProfileContext _context;

    public ProfileRepository(ProfileContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task Create(Profile profile)
    {
        _context.Add(profile);
        return Task.CompletedTask;
    }

    public async Task Delete(ulong discordId)
    {
        var profile = await this.GetByDiscordId(discordId);
        _context.Profiles.Remove(profile);
    }

    public Task<Profile> GetByDiscordId(ulong discordId) => _context.Profiles.FirstOrDefaultAsync(x => x.DiscrodId.Equals(discordId));

    public Task<IEnumerable<Profile>> GetProfiles(int startPosition, int count)
    {
        if (startPosition < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(startPosition));
        }

        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        return Task.FromResult((IEnumerable<Profile>)_context.Profiles.Skip(startPosition).Take(count).ToArray());
    }
}

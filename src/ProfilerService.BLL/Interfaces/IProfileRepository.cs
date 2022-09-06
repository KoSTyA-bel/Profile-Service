using ProfilerService.BLL.Entities;

namespace ProfilerService.BLL.Interfaces;

public interface IProfileRepository
{
    /// todo: remove public
    Task<Profile> GetByDiscordId(ulong discordId);

    Task Create(Profile profile);

    public Task<IEnumerable<Profile>> GetProfiles(int startPosition, int count);

    public Task Delete(ulong discordId);
}

using ProfilerService.BLL.Entities;

namespace ProfilerService.BLL.Interfaces;

public interface IProfileRepository
{
    public Task<Profile> GetByDiscordId(ulong discordId);

    public Task Create(Profile profile);

    public Task<IEnumerable<Profile>> GetProfiles(int startPosition, int count);

    public Task Delete(ulong discordId);
}

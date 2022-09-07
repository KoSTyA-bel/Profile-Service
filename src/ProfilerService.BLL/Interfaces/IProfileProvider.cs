using ProfilerService.BLL.Entities;

namespace ProfilerService.BLL.Interfaces;

public interface IProfileProvider
{
    Task<Profile> GetByDiscordId(ulong discordId, CancellationToken token);

    Task<IEnumerable<Profile>> GetProfiles(int page, int pageSize, CancellationToken token);
}

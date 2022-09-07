using ProfilerService.BLL.Entities;

namespace ProfilerService.BLL.Interfaces;

public interface IProfileRepository
{
    Task Create(Profile profile, CancellationToken token);

    Task Delete(Profile discordId, CancellationToken token);
}

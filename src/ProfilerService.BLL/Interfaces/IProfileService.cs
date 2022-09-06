using ProfilerService.BLL.Entities;

namespace ProfilerService.BLL.Interfaces;

public interface IProfileService
{
    public Task<Profile> GetByDiscordId(ulong discordId);

    public Task<Profile> Create(Profile profile);

    public Task<IEnumerable<Profile>> GetProfiles(int startPosition, int count);

    public Task<bool> Delete(ulong discordId);

    public Task<bool> DepositPoints(ulong discordId, double points);

    public Task<bool> WithdrawPoints(ulong discordId, double points);
}

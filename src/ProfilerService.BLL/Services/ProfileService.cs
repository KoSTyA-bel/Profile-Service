using ProfilerService.BLL.Entities;
using ProfilerService.BLL.Interfaces;

namespace ProfilerService.BLL.Services;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _repository;
    private readonly IDataContext _dataContext;

    public ProfileService(IProfileRepository profileRepository, IDataContext dataContext)
    {
        _repository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
        _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
    }

    // TODO: take ct from args for each async method

    public async Task<Profile> Create(Profile profile)
    {
        await _repository.Create(profile);
        await _dataContext.SaveChangesAsync(CancellationToken.None);
        return profile;
    }

    public async Task<bool> Delete(ulong discordId)
    {
        await _repository.Delete(discordId);
        await _dataContext.SaveChangesAsync(CancellationToken.None);
        return true;
    }

    public async Task<bool> DepositPoints(ulong discordId, double points)
    {

        // todo: provider: g
        // todo repository: insert, delete, update

        var profile = await _repository.GetByDiscordId(discordId);

        profile.Points += points;

        await _dataContext.SaveChangesAsync(CancellationToken.None);

        return true;
    }

    public Task<Profile> GetByDiscordId(ulong discordId) => _repository.GetByDiscordId(discordId);

    public Task<IEnumerable<Profile>> GetProfiles(int startPosition, int count) => _repository.GetProfiles(startPosition, count);

    public async Task<bool> WithdrawPoints(ulong discordId, double points)
    {
        var profile = await _repository.GetByDiscordId(discordId);

        profile.Points -= points;

        await _dataContext.SaveChangesAsync(CancellationToken.None);

        return true;
    }
}

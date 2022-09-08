using ProfilerService.BLL.Entities;
using ProfilerService.BLL.Interfaces;

namespace ProfilerService.BLL.Services;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _repository;
    private readonly IProfileProvider _provider;
    private readonly IDataContext _dataContext;

    public ProfileService(IProfileRepository profileRepository, IProfileProvider provider, IDataContext dataContext)
    {
        _repository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
    }

    public async Task<Profile> Create(Profile profile, CancellationToken token)
    {
        await _repository.Create(profile, token);
        await _dataContext.SaveChanges(token);
        return profile;
    }

    public async Task<bool> Delete(ulong discordId, CancellationToken token)
    {
        var profile = await _provider.GetByDiscordId(discordId, token);
        await _repository.Delete(profile, token);
        await _dataContext.SaveChanges(token);
        return true;
    }

    public async Task<Status> DepositPoints(ulong discordId, double points, CancellationToken token)
    {
        var profile = await _provider.GetByDiscordId(discordId, token);

        profile.Points += points;

        await _dataContext.SaveChanges(token);

        return Status.Succes;
    }

    public Task<Profile> GetByDiscordId(ulong discordId, CancellationToken token) => _provider.GetByDiscordId(discordId, token);

    public Task<IEnumerable<Profile>> GetProfiles(int startPosition, int count, CancellationToken token) 
    {
        if (startPosition < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(startPosition));
        }

        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        return _provider.GetProfiles(startPosition, count, token); 
    }

    public async Task<Status> WithdrawPoints(ulong discordId, double points, CancellationToken token)
    {
        var profile = await _provider.GetByDiscordId(discordId, token);

        profile.Points -= points;

        await _dataContext.SaveChanges(token);

        return Status.Succes;
    }
}

using ProfilerService.BLL.Entities;
using ProfilerService.BLL.Interfaces;

namespace ProfilerService.BLL.Services;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _repository;
    private readonly IProfileProvider _provider;
    private readonly IDataContext _dataContext;
    private readonly IWithdrawer _withdrawer;
    private readonly IDepositer _depositer;

    public ProfileService(IProfileRepository repository, IProfileProvider provider, IDataContext dataContext, IWithdrawer withdrawer, IDepositer depositer)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        _withdrawer = withdrawer ?? throw new ArgumentNullException(nameof(withdrawer));
        _depositer = depositer ?? throw new ArgumentNullException(nameof(depositer));
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

    public async Task<StatusType> DepositPoints(ulong discordId, int points, CancellationToken token)
    {
        var profile = await _provider.GetByDiscordId(discordId, token);

        _depositer.Deposit(profile, points);

        await _dataContext.SaveChanges(token);

        return StatusType.Success;
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

    public async Task<Profile> Update(Profile profile, CancellationToken token)
    {
        if (profile is null)
        {
            throw new ArgumentNullException(nameof(profile));
        }

        await _repository.Update(profile, token);

        await _dataContext.SaveChanges(token);

        return profile;
    }

    public async Task<StatusType> WithdrawPoints(ulong discordId, int points, CancellationToken token)
    {
        var profile = await _provider.GetByDiscordId(discordId, token);

        _withdrawer.Withdraw(profile, points);

        await _dataContext.SaveChanges(token);

        return StatusType.Success;
    }
}

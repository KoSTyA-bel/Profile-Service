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
    private readonly IDateTimeProvider _timeProvider;

    public ProfileService(IProfileRepository repository, IProfileProvider provider, IDataContext dataContext, IWithdrawer withdrawer, IDepositer depositer, IDateTimeProvider timeProvider)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        _withdrawer = withdrawer ?? throw new ArgumentNullException(nameof(withdrawer));
        _depositer = depositer ?? throw new ArgumentNullException(nameof(depositer));
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
    }

    public async Task<StatusType> Create(Profile profile, CancellationToken token)
    {
        profile.CreationDate = _timeProvider.NowUTC;
        await _repository.Create(profile, token);
        await _dataContext.SaveChanges(token);
        return StatusType.Success;
    }

    public async Task<StatusType> Delete(ulong discordId, CancellationToken token)
    {
        var profile = await _provider.GetByDiscordId(discordId, token);
        await _repository.Delete(profile, token);
        await _dataContext.SaveChanges(token);
        return StatusType.Success;
    }

    public async Task<StatusType> DepositPoints(ulong discordId, int pointsAmount, CancellationToken token)
    {
        var profile = await _provider.GetByDiscordId(discordId, token);

        _depositer.Deposit(profile, pointsAmount);

        await _dataContext.SaveChanges(token);

        return StatusType.Success;
    }

    public Task<Profile> GetByDiscordId(ulong discordId, CancellationToken token) => _provider.GetByDiscordId(discordId, token);

    public async Task<IEnumerable<Profile>> GetLeaderBoard(int count, CancellationToken token)
    {
        var profiles = await _provider.GetAllProfiles(token);

        return profiles.OrderByDescending(profile => profile.PointsAmount).Take(count);
    }

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

    public async Task<StatusType> ResetPoints(int pointsAmount, CancellationToken token)
    {
        var profiles = await _provider.GetAllProfiles(token);

        foreach (var profile in profiles)
        {
            profile.PointsAmount = pointsAmount;
        }

        await _dataContext.SaveChanges(token);

        return StatusType.Success;
    }

    public async Task<StatusType> Update(Profile profile, CancellationToken token)
    {
        if (profile is null)
        {
            throw new ArgumentNullException(nameof(profile));
        }

        await _repository.Update(profile, token);

        await _dataContext.SaveChanges(token);

        return StatusType.Success;
    }

    public async Task<StatusType> WithdrawPoints(ulong discordId, int pointsAmount, CancellationToken token)
    {
        var profile = await _provider.GetByDiscordId(discordId, token);

        _withdrawer.Withdraw(profile, pointsAmount);

        await _dataContext.SaveChanges(token);

        return StatusType.Success;
    }
}

using ProfileService.BLL.Entities;
using ProfileService.BLL.Interfaces;

namespace ProfileService.BLL.Services;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _repository;
    private readonly IProfileProvider _provider;
    private readonly IDataContext _dataContext;
    private readonly IBattleResultCounter _resultCounter;
    private readonly IDateTimeProvider _timeProvider;

    public ProfileService(IProfileRepository repository, IProfileProvider provider, IDataContext dataContext, IBattleResultCounter resultCounter, IDateTimeProvider timeProvider)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        _resultCounter = resultCounter ?? throw new ArgumentNullException(nameof(resultCounter));
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
    }

    public async Task<StatusType> CountBattleResult(ulong discordId, int pointsAmount, BattleExodus exodus, CancellationToken token)
    {
        var profile = await _provider.GetByDiscordId(discordId, token);

        switch (exodus)
        {
            case BattleExodus.Win:
                _resultCounter.CountVictory(profile, pointsAmount);
                break;
            case BattleExodus.Lose:
                _resultCounter.CountLose(profile, pointsAmount);
                break;
            default:
                return StatusType.Failed;
        }

        await _dataContext.SaveChanges(token);

        return StatusType.Success;
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

        if (profile is null)
        {
            return StatusType.Failed;
        }

        _resultCounter.Deposit(profile, pointsAmount);

        await _dataContext.SaveChanges(token);

        return StatusType.Success;
    }

    public Task<Profile> GetByDiscordId(ulong discordId, CancellationToken token)
    {
        return _provider.GetByDiscordId(discordId, token);
    }

    public async Task<IEnumerable<Profile>> GetLeaderBoard(int count, CancellationToken token)
    {
        var profiles = await _provider.GetAllProfiles(token);

        return profiles.OrderByDescending(profile => profile.PointsAmount).Take(count);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:Преобразовать в условное выражение", Justification = "<Ожидание>")]
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

    public async Task<StatusType> ResetBattleResults(uint winCount, uint loseCount, CancellationToken token)
    {
        var profiles = await _provider.GetAllProfiles(token);

        foreach (var profile in profiles)
        {
            _resultCounter.ResetBattleResult(profile, winCount, loseCount);
        }

        await _dataContext.SaveChanges(token);

        return StatusType.Success;
    }

    public async Task<StatusType> ResetPoints(int pointsAmount, CancellationToken token)
    {
        var profiles = await _provider.GetAllProfiles(token);

        foreach (var profile in profiles)
        {
            _resultCounter.ResetPoints(profile, pointsAmount);
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

        if (profile is null)
        {
            return StatusType.Failed;
        }

        _resultCounter.Withdraw(profile, pointsAmount);

        await _dataContext.SaveChanges(token);

        return StatusType.Success;
    }
}

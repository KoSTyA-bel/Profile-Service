using Microsoft.Extensions.Logging;
using ProfileService.BLL.Interfaces;
using ProfileService.BLL.Entities;
using ProfileService.BLL.Interfaces;

namespace ProfileService.BLL.BusinessLogic;

public class Business : IBattleResultCounter
{
    private readonly ILogger<Business> _logger;

    public Business(ILogger<Business> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void CountLose(Profile profile, int pointsAmount)
    {
        _logger.LogTrace("Count lose to profile with id={profile.Id}, discordId={profile.DiscordId}", profile.Id, profile.DiscrodId);

        try
        {
            checked
            {
                profile.LoseCount += 1;
            }
        }
        catch (OverflowException)
        {
            profile.LoseCount = uint.MaxValue;
        }

        Withdraw(profile, pointsAmount);
    }

    public void CountVictory(Profile profile, int pointsAmount)
    {
        _logger.LogTrace("Count victory to profile with id={profile.Id}, discordId={profile.DiscordId}", profile.Id, profile.DiscrodId);

        try
        {
            checked
            {
                profile.WinCount += 1;
            }
        }
        catch (OverflowException)
        {
            profile.WinCount = uint.MaxValue;
        }

        Deposit(profile, pointsAmount);
    }

    public void Deposit(Profile profile, int pointsAmount)
    {
        _logger.LogTrace("Deposit points points={points} to profile with id={profile.Id}, discordId={profile.DiscordId}", pointsAmount, profile.Id, profile.DiscrodId);

        try
        {
            checked
            {
                profile.PointsAmount += pointsAmount;
            }
        }
        catch (OverflowException)
        {
            profile.PointsAmount = int.MaxValue;
        }
    }

    public void Withdraw(Profile profile, int pointsAmount)
    {
        _logger.LogTrace("Withdraw points points={points} form profile with id={profile.Id}, discordId={profile.DiscordId}", pointsAmount, profile.Id, profile.DiscrodId);

        if (profile.PointsAmount <= 0)
        {
            return;
        }

        profile.PointsAmount -= pointsAmount;
    }
}

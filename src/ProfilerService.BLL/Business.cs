using Microsoft.Extensions.Logging;
using ProfilerService.BLL.Entities;
using ProfilerService.BLL.Interfaces;

namespace ProfilerService.BLL;

public class Business : IDepositer, IWithdrawer
{
    private readonly ILogger<Business> _logger;

    public Business(ILogger<Business> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void Deposit(Profile profile, double points)
    {
        _logger.LogTrace("Deposit points points={points} to profile with id={profile.Id}, discordId={profile.DiscordId}", points, profile.Id, profile.DiscrodId);
        profile.Points += points;
    }

    public void Withdraw(Profile profile, double points)
    {
        _logger.LogTrace("Withdraw points points={points} form profile with id={profile.Id}, discordId={profile.DiscordId}", points, profile.Id, profile.DiscrodId);
        profile.Points -= points;
    }
}

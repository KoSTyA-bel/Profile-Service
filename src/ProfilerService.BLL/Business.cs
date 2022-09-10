using ProfilerService.BLL.Entities;
using ProfilerService.BLL.Interfaces;

namespace ProfilerService.BLL;

public class Business : IDepositer, IWithdrawer
{
    public void Deposit(Profile profile, double points)
    {
        profile.Points += points;
    }

    public void Withdraw(Profile profile, double points)
    {
        profile.Points -= points;
    }
}

using ProfilerService.BLL.Entities;

namespace ProfilerService.BLL.Interfaces;

public interface IWithdrawer
{
    void Withdraw(Profile profile, double points);
}

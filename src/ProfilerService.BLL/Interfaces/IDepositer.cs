using ProfilerService.BLL.Entities;

namespace ProfilerService.BLL.Interfaces;

public interface IDepositer
{
    void Deposit(Profile profile, int points);
}

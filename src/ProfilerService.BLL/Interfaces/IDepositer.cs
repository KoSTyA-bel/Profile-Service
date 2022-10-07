using ProfileService.BLL.Entities;

namespace ProfileService.BLL.Interfaces;

public interface IDepositer
{
    void Deposit(Profile profile, int pointsAmount);
}

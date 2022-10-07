using ProfileService.BLL.Entities;

namespace ProfileService.BLL.Interfaces;

public interface IWithdrawer
{
    void Withdraw(Profile profile, int pointsAmount);
}

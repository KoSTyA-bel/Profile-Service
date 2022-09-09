using ProfilerService.BLL.Entities;

namespace ProfilerService.BLL.Interfaces;

public interface IWaxWalletVerifyService
{
    Task<StatusType> VerifyWaxWallet(string waxWallet, CancellationToken token);
}

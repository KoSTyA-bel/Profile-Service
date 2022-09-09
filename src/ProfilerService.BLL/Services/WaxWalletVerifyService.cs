using ProfilerService.BLL.Entities;
using ProfilerService.BLL.Interfaces;
using ProfilerService.BLL.Settings;

namespace ProfilerService.BLL.Services;

public class WaxWalletVerifyService : IWaxWalletVerifyService
{
    private readonly IWaxWalletVerifier _verifyer;

    public WaxWalletVerifyService(IWaxWalletVerifier verifyer)
    {
        _verifyer = verifyer ?? throw new ArgumentNullException(nameof(verifyer));
    }

    public async Task<StatusType> VerifyWaxWallet(string waxWallet, CancellationToken token)
    {
        var result = await _verifyer.VerifyWaxWallet(waxWallet, token);

        return result ? StatusType.Succes : StatusType.Failed;
    }
}

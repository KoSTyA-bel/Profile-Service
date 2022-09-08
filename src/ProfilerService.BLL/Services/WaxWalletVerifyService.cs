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

    public async Task<Status> VerifyWaxWallet(string waxWallet)
    {
        var result = await _verifyer.VerifyWaxWallet(waxWallet);

        return result ? Status.Succes : Status.Failed;
    }
}

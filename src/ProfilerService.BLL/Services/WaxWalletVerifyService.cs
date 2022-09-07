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

    public Task<bool> VerifyWaxWallet(string waxWallet) => _verifyer.VerifyWaxWallet(waxWallet);
}

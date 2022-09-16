using ProfilerService.BLL.Entities;
using ProfilerService.BLL.Interfaces;
using ProfilerService.BLL.Settings;

namespace ProfilerService.BLL.Services;

public class NFTVerifyService : INFTVerifyService
{
    private readonly INFTVerifier _verifyer;

    public NFTVerifyService(INFTVerifier verifyer)
    {
        _verifyer = verifyer ?? throw new ArgumentNullException(nameof(verifyer));
    }

    public Task<NFTType> VerifyWaxWallet(string waxWallet, CancellationToken token)
    {
        return _verifyer.VerifyWaxWallet(waxWallet, token);
    }
}

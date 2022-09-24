using ProfilerService.BLL.Entities;

namespace ProfilerService.BLL.Interfaces;

public interface INFTVerifyService
{
    Task<NFTType> VerifyWaxWallet(string waxWallet, CancellationToken token);

    Task<IEnumerable<NFTType>> VerifyWaxWallets(IEnumerable<string> waxWallets, CancellationToken token);
}

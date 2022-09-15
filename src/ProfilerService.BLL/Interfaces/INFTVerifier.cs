using ProfilerService.BLL.Entities;

namespace ProfilerService.BLL.Interfaces;

public interface INFTVerifier
{
    Task<NFTType> VerifyWaxWallet(string waxWallet, CancellationToken token);
}

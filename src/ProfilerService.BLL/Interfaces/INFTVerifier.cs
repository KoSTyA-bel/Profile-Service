using ProfileService.BLL.Entities;

namespace ProfileService.BLL.Interfaces;

public interface INFTVerifier
{
    Task<NFTType> VerifyWaxWallet(string waxWallet, CancellationToken token);
}

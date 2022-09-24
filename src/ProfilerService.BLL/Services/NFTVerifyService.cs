using ProfilerService.BLL.Entities;
using ProfilerService.BLL.Interfaces;
using ProfilerService.BLL.Settings;
using System.Collections.Generic;

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

    public Task<IEnumerable<NFTType>> VerifyWaxWallets(IEnumerable<string> waxWallets, CancellationToken token)
    {
        var count = waxWallets.Count();
        var types = new System.Collections.Concurrent.ConcurrentStack<NFTType>();

        Parallel.For(0, count, i =>
        {
            types.Push(_verifyer.VerifyWaxWallet(waxWallets.Skip(i).First(), token).GetAwaiter().GetResult());
        });

        var result = types.ToArray();

        return Task.FromResult((IEnumerable<NFTType>)types);
    }
}

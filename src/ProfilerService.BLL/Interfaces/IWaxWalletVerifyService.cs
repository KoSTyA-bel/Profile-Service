namespace ProfilerService.BLL.Interfaces;

public interface IWaxWalletVerifyService
{
    Task<bool> VerifyWaxWallet(string waxWallet);
}

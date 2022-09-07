namespace ProfilerService.BLL.Interfaces;

public interface IWaxWalletVerifier
{
    Task<bool> VerifyWaxWallet(string waxWallet);
}

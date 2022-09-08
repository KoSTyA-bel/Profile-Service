using ProfilerService.BLL.Interfaces;

namespace ProfilerService.BLL.Settings;

public class WaxWalletVerifierSettings : IWaxWalletVerifierSettings
{
    public string ApiUrl { get; set; }

    public string CollectionName { get; set; }
}

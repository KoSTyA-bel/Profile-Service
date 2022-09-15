using ProfilerService.BLL.Interfaces;

namespace ProfilerService.BLL.Settings;

public class NFTVerifierSettings : INFTVerifierSettings
{
    public string ApiUrl { get; set; }

    public string CollectionName { get; set; }

    public string CommonTemplate { get; set; }

    public string RareTemplate { get; set; }

    public string EpicTemplate { get; set; }
}

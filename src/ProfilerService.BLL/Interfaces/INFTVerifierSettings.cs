namespace ProfilerService.BLL.Interfaces;

public interface INFTVerifierSettings
{
    string ApiUrl { get; set; }

    string CollectionName { get; set; }

    string CommonTemplate { get; set; }

    string RareTemplate { get; set; }

    string EpicTemplate { get; set; }
}

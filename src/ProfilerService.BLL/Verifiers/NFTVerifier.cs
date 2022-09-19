using ProfilerService.BLL.Entities;
using ProfilerService.BLL.Interfaces;
using ProfilerService.BLL.Settings;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProfilerService.BLL.Verifiers;

public class NFTVerifier : INFTVerifier
{
    private readonly INFTVerifierSettings _settings;

    public NFTVerifier(INFTVerifierSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    public async Task<NFTType> VerifyWaxWallet(string waxWallet, CancellationToken token)
    {
        using var client = new HttpClient();
        var uri = _settings.ApiUrl + "&owner=" + waxWallet + "&collection_name=" + _settings.CollectionName;

        client.BaseAddress = new Uri(uri);

        var acceptedHeader = new MediaTypeWithQualityHeaderValue("application/json");

        client.DefaultRequestHeaders.Accept.Add(acceptedHeader);

        var response = await client.GetAsync(string.Empty, token);

        if (!response.IsSuccessStatusCode)
        {
            return NFTType.Unspecified;
        }

        var streamTask = response.Content.ReadAsStreamAsync();

        var waxApiResponse = await JsonSerializer.DeserializeAsync<WaxApiResponse>(await streamTask, cancellationToken: token);

        var nfts = waxApiResponse.data;

        if (nfts.Length == 0)
        {
            return NFTType.Unspecified;
        }

        var result = NFTType.Unspecified;

        foreach (var nft in nfts)
        {
            if (nft.template.template_id == _settings.CommonTemplate)
            {
                result = result >= NFTType.Common ? result : NFTType.Common;
            }
            else if (nft.template.template_id == _settings.RareTemplate)
            {
                result = result >= NFTType.Rare ? result : NFTType.Rare;
            }
            else if (nft.template.template_id == _settings.EpicTemplate)
            {
                result = NFTType.Epic;
                break;
            }
        }

        return result;
    }
}

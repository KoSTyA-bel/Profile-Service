using ProfilerService.BLL.Entities;
using ProfilerService.BLL.Interfaces;
using ProfilerService.BLL.Settings;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProfilerService.BLL.Verifiers;

public class WaxWalletVerifier : IWaxWalletVerifier
{
    private readonly IWaxWalletVerifierSettings _settings;

    public WaxWalletVerifier(IWaxWalletVerifierSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    public async Task<bool> VerifyWaxWallet(string waxWallet)
    {
        using var client = new HttpClient();
        var uri = _settings.ApiUrl + "&owner=" + waxWallet + "&collection_name=" + _settings.CollectionName;

        client.BaseAddress = new Uri(uri);

        var acceptedHeader = new MediaTypeWithQualityHeaderValue("application/json");

        client.DefaultRequestHeaders.Accept.Add(acceptedHeader);

        var response = await client.GetAsync(string.Empty);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var streamTask = response.Content.ReadAsStreamAsync();

        var waxApiResponse = await JsonSerializer.DeserializeAsync<WaxApiResponse>(await streamTask);

        return waxApiResponse.data.Length != 0;
    }
}

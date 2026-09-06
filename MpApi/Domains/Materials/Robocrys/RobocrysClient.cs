using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Materials.Robocrys.Models;

namespace MpApi.Domains.Materials.Robocrys;

public sealed class RobocrysClient : IRobocrysClient
{
    private const string BaseEndpoint = "robocrys/";
    private readonly IMpHttpClient _httpClient;

    public RobocrysClient(IMpHttpClient httpClient) => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<RobocrysDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?> { ["material_ids"] = materialId };
        var response = await _httpClient.GetAsync<IReadOnlyList<RobocrysDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }
}
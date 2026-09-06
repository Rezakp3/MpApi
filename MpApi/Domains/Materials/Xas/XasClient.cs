using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Materials.Xas.Models;

namespace MpApi.Domains.Materials.Xas;

public sealed class XasClient : IXasClient
{
    private const string BaseEndpoint = "materials/xas/";
    private readonly IMpHttpClient _httpClient;

    public XasClient(IMpHttpClient httpClient) => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<MpResponse<IReadOnlyList<XasDoc>>> SearchAsync(string materialId, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?> { ["material_ids"] = materialId };
        return await _httpClient.GetAsync<IReadOnlyList<XasDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
    }
}
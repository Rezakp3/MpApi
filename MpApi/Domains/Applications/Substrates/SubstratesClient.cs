using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Applications.Substrates.Models;

namespace MpApi.Domains.Applications.Substrates;

public sealed class SubstratesClient : ISubstratesClient
{
    private const string BaseEndpoint = "materials/substrates/";
    private readonly IMpHttpClient _httpClient;

    public SubstratesClient(IMpHttpClient httpClient) => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<MpResponse<IReadOnlyList<SubstrateDoc>>> SearchAsync(SubstrateSearchFilter filter, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);
        return await _httpClient.GetAsync<IReadOnlyList<SubstrateDoc>>(BaseEndpoint, filter.ToQueryParameters(), cancellationToken).ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<SubstrateDoc>> GetMatchesForMaterialAsync(string materialId, CancellationToken cancellationToken = default)
    {
        var filter = new SubstrateSearchFilter { MaterialId = materialId, Limit = 100 };
        var response = await SearchAsync(filter, cancellationToken).ConfigureAwait(false);
        return response.Data ?? Array.Empty<SubstrateDoc>();
    }
}
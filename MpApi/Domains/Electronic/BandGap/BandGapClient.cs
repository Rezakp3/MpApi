using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Electronic.BandGap.Models;

namespace MpApi.Domains.Electronic.BandGap;

public sealed class BandGapClient : IBandGapClient
{
    private const string BaseEndpoint = "materials/electronic_structure_bandgap/";
    private readonly IMpHttpClient _httpClient;

    public BandGapClient(IMpHttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<BandGapDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(materialId)) throw new ArgumentException("Material ID cannot be empty.", nameof(materialId));

        var query = new Dictionary<string, string?> { ["material_ids"] = materialId };
        var response = await _httpClient.GetAsync<IReadOnlyList<BandGapDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }

    public async Task<MpResponse<IReadOnlyList<BandGapDoc>>> SearchAsync(BandGapSearchFilter filter, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);
        return await _httpClient.GetAsync<IReadOnlyList<BandGapDoc>>(BaseEndpoint, filter.ToQueryParameters(), cancellationToken).ConfigureAwait(false);
    }
}
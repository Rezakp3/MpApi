using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Electronic.Dielectric.Models;

namespace MpApi.Domains.Electronic.Dielectric;

public sealed class DielectricClient : IDielectricClient
{
    private const string BaseEndpoint = "materials/dielectric/";
    private readonly IMpHttpClient _httpClient;

    public DielectricClient(IMpHttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<DielectricDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(materialId)) throw new ArgumentException("Material ID cannot be empty.", nameof(materialId));

        var query = new Dictionary<string, string?> { ["material_ids"] = materialId };
        var response = await _httpClient.GetAsync<IReadOnlyList<DielectricDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }

    public async Task<MpResponse<IReadOnlyList<DielectricDoc>>> SearchAsync(DielectricSearchFilter filter, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);
        return await _httpClient.GetAsync<IReadOnlyList<DielectricDoc>>(BaseEndpoint, filter.ToQueryParameters(), cancellationToken).ConfigureAwait(false);
    }
}
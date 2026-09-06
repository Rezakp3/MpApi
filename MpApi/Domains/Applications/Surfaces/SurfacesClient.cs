using MpApi.Common.Http;
using MpApi.Common.Models;

namespace MpApi.Domains.Applications.Surfaces;

public sealed class SurfacesClient : ISurfacesClient
{
    private const string BaseEndpoint = "materials/surface_properties/";
    private readonly IMpHttpClient _httpClient;

    public SurfacesClient(IMpHttpClient httpClient) => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<SurfaceDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?> { ["material_ids"] = materialId };
        var response = await _httpClient.GetAsync<IReadOnlyList<SurfaceDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }

    public async Task<MpResponse<IReadOnlyList<SurfaceDoc>>> SearchAsync(string? chemsys = null, int limit = 50, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?> { ["_limit"] = limit.ToString() };
        if (!string.IsNullOrWhiteSpace(chemsys)) query["chemsys"] = chemsys;
        return await _httpClient.GetAsync<IReadOnlyList<SurfaceDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
    }
}
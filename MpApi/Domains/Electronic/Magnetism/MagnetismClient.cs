using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Electronic.Magnetism.Models;

namespace MpApi.Domains.Electronic.Magnetism;

public sealed class MagnetismClient : IMagnetismClient
{
    private const string BaseEndpoint = "materials/magnetism/";
    private readonly IMpHttpClient _httpClient;

    public MagnetismClient(IMpHttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<MagnetismDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(materialId)) throw new ArgumentException("Material ID cannot be empty.", nameof(materialId));

        var query = new Dictionary<string, string?> { ["material_ids"] = materialId };
        var response = await _httpClient.GetAsync<IReadOnlyList<MagnetismDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }

    public async Task<MpResponse<IReadOnlyList<MagnetismDoc>>> SearchAsync(MagnetismSearchFilter filter, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);
        return await _httpClient.GetAsync<IReadOnlyList<MagnetismDoc>>(BaseEndpoint, filter.ToQueryParameters(), cancellationToken).ConfigureAwait(false);
    }
}
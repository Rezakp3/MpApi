using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Materials.Structures.Models;

namespace MpApi.Domains.Materials.Structures;

/// <summary>
/// Implementation of <see cref="IStructuresClient"/> querying the materials structures endpoint.
/// </summary>
public sealed class StructuresClient : IStructuresClient
{
    private const string BaseEndpoint = "materials/core/";
    private readonly IMpHttpClient _httpClient;

    public StructuresClient(IMpHttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<MaterialStructureDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(materialId))
        {
            throw new ArgumentException("Material ID cannot be null or empty.", nameof(materialId));
        }

        var queryParams = new Dictionary<string, string?>
        {
            ["material_ids"] = materialId
        };

        var response = await _httpClient.GetAsync<IReadOnlyList<MaterialStructureDoc>>(BaseEndpoint, queryParams, cancellationToken)
            .ConfigureAwait(false);

        return response.Data?.FirstOrDefault();
    }

    public async Task<MpResponse<IReadOnlyList<MaterialStructureDoc>>> SearchAsync(
        StructureSearchFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        return await _httpClient.GetAsync<IReadOnlyList<MaterialStructureDoc>>(
            BaseEndpoint,
            filter.ToQueryParameters(),
            cancellationToken).ConfigureAwait(false);
    }

    public async Task<string> GetCifAsync(string materialId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(materialId))
        {
            throw new ArgumentException("Material ID cannot be null or empty.", nameof(materialId));
        }

        // Fetching the CIF string field directly from the endpoint
        var queryParams = new Dictionary<string, string?>
        {
            ["material_ids"] = materialId,
            ["_fields"] = "cif"
        };

        var response = await _httpClient.GetAsync<IReadOnlyList<MaterialStructureDoc>>(BaseEndpoint, queryParams, cancellationToken)
            .ConfigureAwait(false);

        var doc = response.Data?.FirstOrDefault();
        return doc?.Cif ?? string.Empty;
    }
}
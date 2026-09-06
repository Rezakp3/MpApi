using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Electronic.ElectronicStructure.Models;

namespace MpApi.Domains.Electronic.ElectronicStructure;

public sealed class ElectronicStructureClient : IElectronicStructureClient
{
    private const string BaseEndpoint = "materials/electronic_structure/";
    private readonly IMpHttpClient _httpClient;

    public ElectronicStructureClient(IMpHttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<ElectronicStructureDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(materialId)) throw new ArgumentException("Material ID cannot be empty.", nameof(materialId));

        var query = new Dictionary<string, string?> { ["material_ids"] = materialId };
        var response = await _httpClient.GetAsync<IReadOnlyList<ElectronicStructureDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }

    public async Task<MpResponse<IReadOnlyList<ElectronicStructureDoc>>> SearchAsync(ElectronicStructureSearchFilter filter, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);
        return await _httpClient.GetAsync<IReadOnlyList<ElectronicStructureDoc>>(BaseEndpoint, filter.ToQueryParameters(), cancellationToken).ConfigureAwait(false);
    }
}
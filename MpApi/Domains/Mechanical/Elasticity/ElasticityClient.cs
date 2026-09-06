using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Mechanical.Elasticity.Models;

namespace MpApi.Domains.Mechanical.Elasticity;

public sealed class ElasticityClient : IElasticityClient
{
    private const string BaseEndpoint = "materials/elasticity/";
    private readonly IMpHttpClient _httpClient;

    public ElasticityClient(IMpHttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<ElasticityDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(materialId)) throw new ArgumentException("Material ID cannot be empty.", nameof(materialId));

        var query = new Dictionary<string, string?> { ["material_ids"] = materialId };
        var response = await _httpClient.GetAsync<IReadOnlyList<ElasticityDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }

    public async Task<MpResponse<IReadOnlyList<ElasticityDoc>>> SearchAsync(ElasticitySearchFilter filter, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);
        return await _httpClient.GetAsync<IReadOnlyList<ElasticityDoc>>(BaseEndpoint, filter.ToQueryParameters(), cancellationToken).ConfigureAwait(false);
    }
}
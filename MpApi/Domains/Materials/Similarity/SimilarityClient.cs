using MpApi.Common.Http;
using MpApi.Domains.Materials.Similarity.Models;

namespace MpApi.Domains.Materials.Similarity;

public sealed class SimilarityClient : ISimilarityClient
{
    private const string BaseEndpoint = "materials/similarity/";
    private readonly IMpHttpClient _httpClient;

    public SimilarityClient(IMpHttpClient httpClient) => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<SimilarityDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?> { ["material_ids"] = materialId };
        var response = await _httpClient.GetAsync<IReadOnlyList<SimilarityDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }
}
using MpApi.Common.Http;

namespace MpApi.Domains.Materials.Chemenv;

public sealed class ChemenvClient : IChemenvClient
{
    private const string BaseEndpoint = "materials/chemenv/";
    private readonly IMpHttpClient _httpClient;

    public ChemenvClient(IMpHttpClient httpClient) => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<ChemenvDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?> { ["material_ids"] = materialId };
        var response = await _httpClient.GetAsync<IReadOnlyList<ChemenvDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }
}
using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Applications.Alloys.Models;

namespace MpApi.Domains.Applications.Alloys;

public sealed class AlloysClient : IAlloysClient
{
    private const string BaseEndpoint = "materials/alloys/";
    private readonly IMpHttpClient _httpClient;

    public AlloysClient(IMpHttpClient httpClient) => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<MpResponse<IReadOnlyList<AlloyDoc>>> SearchAsync(string? formula = null, int limit = 50, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?> { ["_limit"] = limit.ToString() };
        if (!string.IsNullOrWhiteSpace(formula)) query["formula"] = formula;
        return await _httpClient.GetAsync<IReadOnlyList<AlloyDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
    }
}
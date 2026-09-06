using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Applications.Synthesis.Models;

namespace MpApi.Domains.Applications.Synthesis;

public sealed class SynthesisClient : ISynthesisClient
{
    private const string BaseEndpoint = "synthesis/";
    private readonly IMpHttpClient _httpClient;

    public SynthesisClient(IMpHttpClient httpClient) => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<MpResponse<IReadOnlyList<SynthesisDoc>>> SearchByTargetFormulaAsync(string targetFormula, int limit = 20, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?>
        {
            ["keywords"] = targetFormula,
            ["_limit"] = limit.ToString()
        };
        return await _httpClient.GetAsync<IReadOnlyList<SynthesisDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
    }
}
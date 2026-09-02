using MpApi.Core.Http;
using MpApi.Synthesis.Models;

namespace MpApi.Synthesis;

public sealed class MpSynthesisClient(IMpHttpClient http) : IMpSynthesisClient
{
    private const string SynthesisEndpoint = "synthesis/";
    private readonly IMpHttpClient _http = http ?? throw new ArgumentNullException(nameof(http));

    public async Task<IReadOnlyList<SynthesisRecipe>> SearchByTargetAsync(string targetFormula, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(targetFormula);

        var query = new Dictionary<string, object?> { ["target_formula"] = targetFormula };
        var res = await _http.GetAsync<SynthesisRecipe>(SynthesisEndpoint, query, cancellationToken).ConfigureAwait(false);
        return res.Data;
    }

    public IAsyncEnumerable<SynthesisRecipe> StreamRecipesAsync(
        Dictionary<string, object?> queryParams,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(queryParams);
        return _http.StreamAsync<SynthesisRecipe>(SynthesisEndpoint, queryParams, 100, cancellationToken);
    }
}
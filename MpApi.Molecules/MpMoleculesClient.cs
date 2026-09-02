using MpApi.Core.Http;
using MpApi.Molecules.Models;

namespace MpApi.Molecules;

public sealed class MpMoleculesClient(IMpHttpClient http) : IMpMoleculesClient
{
    private const string MoleculesEndpoint = "molecules/summary/";
    private readonly IMpHttpClient _http = http ?? throw new ArgumentNullException(nameof(http));

    public async Task<MoleculeSummary?> GetByIdAsync(string moleculeId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(moleculeId);

        var query = new Dictionary<string, object?> { ["task_ids"] = moleculeId, ["_limit"] = 1 };
        var res = await _http.GetAsync<MoleculeSummary>(MoleculesEndpoint, query, cancellationToken).ConfigureAwait(false);
        return res.Data.FirstOrDefault();
    }

    public async Task<IReadOnlyList<MoleculeSummary>> SearchBySmilesAsync(string smiles, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(smiles);

        var query = new Dictionary<string, object?> { ["smiles"] = smiles };
        var res = await _http.GetAsync<MoleculeSummary>(MoleculesEndpoint, query, cancellationToken).ConfigureAwait(false);
        return res.Data;
    }

    public IAsyncEnumerable<MoleculeSummary> StreamSearchAsync(
        Dictionary<string, object?> queryParams,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(queryParams);
        return _http.StreamAsync<MoleculeSummary>(MoleculesEndpoint, queryParams, 100, cancellationToken);
    }
}
using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Molecules.Summary.Models;

namespace MpApi.Domains.Molecules.Summary;

public sealed class MoleculeSummaryClient(IMpHttpClient httpClient) : IMoleculeSummaryClient
{
    private const string BaseEndpoint = "molecules/summary/";
    private readonly IMpHttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<MoleculeSummaryDoc?> GetByIdAsync(string moleculeId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(moleculeId)) throw new ArgumentException("Molecule ID cannot be empty.", nameof(moleculeId));
        var query = new Dictionary<string, string?> { ["molecule_ids"] = moleculeId };
        var response = await _httpClient.GetAsync<IReadOnlyList<MoleculeSummaryDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }

    public async Task<MpResponse<IReadOnlyList<MoleculeSummaryDoc>>> SearchAsync(MoleculeSummarySearchFilter filter, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);
        return await _httpClient.GetAsync<IReadOnlyList<MoleculeSummaryDoc>>(BaseEndpoint, filter.ToQueryParameters(), cancellationToken).ConfigureAwait(false);
    }
}
using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Molecules.Thermo.Models;

namespace MpApi.Domains.Molecules.Thermo;

public sealed class MoleculeThermoClient : IMoleculeThermoClient
{
    private const string BaseEndpoint = "molecules/thermo/";
    private readonly IMpHttpClient _httpClient;

    public MoleculeThermoClient(IMpHttpClient httpClient) => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<MoleculeThermoDoc?> GetByIdAsync(string moleculeId, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?> { ["molecule_ids"] = moleculeId };
        var response = await _httpClient.GetAsync<IReadOnlyList<MoleculeThermoDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }

    public async Task<MpResponse<IReadOnlyList<MoleculeThermoDoc>>> SearchAsync(string? formula = null, int limit = 20, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?> { ["_limit"] = limit.ToString() };
        if (!string.IsNullOrWhiteSpace(formula)) query["formula"] = formula;
        return await _httpClient.GetAsync<IReadOnlyList<MoleculeThermoDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
    }
}
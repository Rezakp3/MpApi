using MpApi.Core.Http;
using MpApi.Thermo.Models;

namespace MpApi.Thermo;

/// <summary>
/// Client for interacting with thermodynamics endpoints.
/// </summary>
public sealed class MpThermoClient(IMpHttpClient http) : IMpThermoClient
{
    private const string ThermoEndpoint = "materials/thermo/";
    private readonly IMpHttpClient _http = http ?? throw new ArgumentNullException(nameof(http));

    public async Task<ThermoData?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(materialId);

        var criteria = new ThermoSearchCriteria
        {
            MaterialIds = [materialId],
            Limit = 1
        };

        var response = await _http.GetAsync<ThermoData>(
            ThermoEndpoint,
            criteria.ToQueryParameters(),
            cancellationToken).ConfigureAwait(false);

        return response.Data.FirstOrDefault();
    }

    public async Task<IReadOnlyList<ThermoData>> SearchAsync(
        ThermoSearchCriteria criteria,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(criteria);

        var response = await _http.GetAsync<ThermoData>(
            ThermoEndpoint,
            criteria.ToQueryParameters(),
            cancellationToken).ConfigureAwait(false);

        return response.Data;
    }

    public IAsyncEnumerable<ThermoData> StreamSearchAsync(
        ThermoSearchCriteria criteria,
        int pageSize = 100,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(criteria);

        return _http.StreamAsync<ThermoData>(
            ThermoEndpoint,
            criteria.ToQueryParameters(),
            pageSize,
            cancellationToken);
    }
    public async Task<IReadOnlyList<ThermoData>> GetByChemsysAsync(string chemsys, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chemsys);

        var query = new Dictionary<string, object?>
        {
            ["chemsys"] = chemsys
        };

        var res = await _http.GetAsync<ThermoData>("materials/thermo/", query, cancellationToken).ConfigureAwait(false);
        return res.Data;
    }

}
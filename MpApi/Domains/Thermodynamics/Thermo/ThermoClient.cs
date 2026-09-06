using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Thermodynamics.Thermo.Models;

namespace MpApi.Domains.Thermodynamics.Thermo;

/// <summary>
/// Implementation of <see cref="IThermoClient"/> querying the Materials Project thermo endpoint.
/// </summary>
public sealed class ThermoClient : IThermoClient
{
    private const string BaseEndpoint = "materials/thermo/";
    private readonly IMpHttpClient _httpClient;

    public ThermoClient(IMpHttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<ThermoDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(materialId))
        {
            throw new ArgumentException("Material ID cannot be null or empty.", nameof(materialId));
        }

        var queryParams = new Dictionary<string, string?>
        {
            ["material_ids"] = materialId
        };

        var response = await _httpClient.GetAsync<IReadOnlyList<ThermoDoc>>(BaseEndpoint, queryParams, cancellationToken)
            .ConfigureAwait(false);

        return response.Data?.FirstOrDefault();
    }

    public async Task<MpResponse<IReadOnlyList<ThermoDoc>>> SearchAsync(
        ThermoSearchFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        return await _httpClient.GetAsync<IReadOnlyList<ThermoDoc>>(
            BaseEndpoint,
            filter.ToQueryParameters(),
            cancellationToken).ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<ThermoDoc>> GetPhaseDiagramEntriesAsync(
        string chemsys,
        string? thermoType = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(chemsys))
        {
            throw new ArgumentException("Chemical system cannot be null or empty.", nameof(chemsys));
        }

        var filter = new ThermoSearchFilter
        {
            Chemsys = chemsys,
            ThermoType = thermoType,
            Limit = 1000 // Ensure all competing phases are captured for hull calculation
        };

        var response = await SearchAsync(filter, cancellationToken).ConfigureAwait(false);
        return response.Data ?? Array.Empty<ThermoDoc>();
    }
}
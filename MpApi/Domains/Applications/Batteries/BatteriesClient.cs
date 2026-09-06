using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Applications.Batteries.Models;

namespace MpApi.Domains.Applications.Batteries;

public sealed class BatteriesClient : IBatteriesClient
{
    private const string BaseEndpoint = "materials/insertion_electrodes/";
    private readonly IMpHttpClient _httpClient;

    public BatteriesClient(IMpHttpClient httpClient) => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<BatteryDoc?> GetByIdAsync(string batteryId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(batteryId)) throw new ArgumentException("Battery ID cannot be empty.", nameof(batteryId));
        var query = new Dictionary<string, string?> { ["battery_id"] = batteryId };
        var response = await _httpClient.GetAsync<IReadOnlyList<BatteryDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }

    public async Task<MpResponse<IReadOnlyList<BatteryDoc>>> SearchAsync(BatterySearchFilter filter, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);
        return await _httpClient.GetAsync<IReadOnlyList<BatteryDoc>>(BaseEndpoint, filter.ToQueryParameters(), cancellationToken).ConfigureAwait(false);
    }
}
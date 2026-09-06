using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Mechanical.Piezoelectric.Models;

namespace MpApi.Domains.Mechanical.Piezoelectric;

public sealed class PiezoelectricClient : IPiezoelectricClient
{
    private const string BaseEndpoint = "materials/piezoelectric/";
    private readonly IMpHttpClient _httpClient;

    public PiezoelectricClient(IMpHttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<PiezoelectricDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(materialId)) throw new ArgumentException("Material ID cannot be empty.", nameof(materialId));

        var query = new Dictionary<string, string?> { ["material_ids"] = materialId };
        var response = await _httpClient.GetAsync<IReadOnlyList<PiezoelectricDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }

    public async Task<MpResponse<IReadOnlyList<PiezoelectricDoc>>> SearchAsync(PiezoelectricSearchFilter filter, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);
        return await _httpClient.GetAsync<IReadOnlyList<PiezoelectricDoc>>(BaseEndpoint, filter.ToQueryParameters(), cancellationToken).ConfigureAwait(false);
    }
}
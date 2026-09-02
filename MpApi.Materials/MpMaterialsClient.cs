using MpApi.Core.Http;
using MpApi.Materials.Models;

namespace MpApi.Materials;

/// <summary>
/// High-level client for Materials Project materials data.
/// </summary>
public sealed class MpMaterialsClient(IMpHttpClient http) : IMpMaterialsClient
{
    private const string SummaryEndpoint = "materials/summary/";
    private readonly IMpHttpClient _http = http ?? throw new ArgumentNullException(nameof(http));

    public async Task<MaterialSummary?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(materialId);

        var criteria = new SummarySearchCriteria
        {
            MaterialIds = [materialId],
            Limit = 1
        };

        var response = await _http.GetAsync<MaterialSummary>(
            SummaryEndpoint,
            criteria.ToQueryParameters(),
            cancellationToken).ConfigureAwait(false);

        return response.Data.FirstOrDefault();
    }

    public async Task<IReadOnlyList<MaterialSummary>> SearchAsync(
        SummarySearchCriteria criteria,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(criteria);

        var response = await _http.GetAsync<MaterialSummary>(
            SummaryEndpoint,
            criteria.ToQueryParameters(),
            cancellationToken).ConfigureAwait(false);

        return response.Data;
    }

    public IAsyncEnumerable<MaterialSummary> StreamSearchAsync(
        SummarySearchCriteria criteria,
        int pageSize = 100,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(criteria);

        return _http.StreamAsync<MaterialSummary>(
            SummaryEndpoint,
            criteria.ToQueryParameters(),
            pageSize,
            cancellationToken);
    }
    public async Task<ElasticityData?> GetElasticityAsync(string materialId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(materialId);
        var query = new Dictionary<string, object?> { ["material_ids"] = materialId, ["_limit"] = 1 };
        var res = await _http.GetAsync<ElasticityData>("materials/elasticity/", query, cancellationToken).ConfigureAwait(false);
        return res.Data.FirstOrDefault();
    }

    public async Task<DielectricData?> GetDielectricAsync(string materialId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(materialId);
        var query = new Dictionary<string, object?> { ["material_ids"] = materialId, ["_limit"] = 1 };
        var res = await _http.GetAsync<DielectricData>("materials/dielectric/", query, cancellationToken).ConfigureAwait(false);
        return res.Data.FirstOrDefault();
    }

    public async Task<PiezoelectricData?> GetPiezoelectricAsync(string materialId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(materialId);
        var query = new Dictionary<string, object?> { ["material_ids"] = materialId, ["_limit"] = 1 };
        var res = await _http.GetAsync<PiezoelectricData>("materials/piezoelectric/", query, cancellationToken).ConfigureAwait(false);
        return res.Data.FirstOrDefault();
    }

    public async Task<SurfacePropertyData?> GetSurfacePropertiesAsync(string materialId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(materialId);
        var query = new Dictionary<string, object?> { ["material_ids"] = materialId, ["_limit"] = 1 };
        var res = await _http.GetAsync<SurfacePropertyData>("materials/surface_properties/", query, cancellationToken).ConfigureAwait(false);
        return res.Data.FirstOrDefault();
    }

    public async Task<CrystalStructure?> GetStructureAsync(string materialId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(materialId);
        var query = new Dictionary<string, object?> { ["material_ids"] = materialId, ["_limit"] = 1 };
        var res = await _http.GetAsync<CrystalStructure>("materials/core/", query, cancellationToken).ConfigureAwait(false);
        return res.Data.FirstOrDefault();
    }
}
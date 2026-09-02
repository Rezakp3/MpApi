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

        var res = await _http.GetAsync<MaterialCoreData>("materials/core/", query, cancellationToken).ConfigureAwait(false);
        return res.Data.FirstOrDefault()?.Structure;
    }
    public async Task<ElectronicStructureData?> GetElectronicStructureAsync(string materialId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(materialId);
        var query = new Dictionary<string, object?> { ["material_ids"] = materialId, ["_limit"] = 1 };

        var res = await _http.GetAsync<ElectronicStructureData>("materials/electronic_structure/", query, cancellationToken).ConfigureAwait(false);
        return res.Data.FirstOrDefault();
    }
    public async Task<IReadOnlyList<XasData>> GetXasAsync(
    string materialId,
    string? absorbingElement = null,
    string? edge = null,
    CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(materialId);

        var query = new Dictionary<string, object?>
        {
            ["material_ids"] = materialId,
            ["absorbing_element"] = absorbingElement,
            ["edge"] = edge
        };

        var res = await _http.GetAsync<XasData>("materials/xas/", query, cancellationToken).ConfigureAwait(false);
        return res.Data;
    }

    public async Task<IReadOnlyList<SimilarityData>> GetSimilarMaterialsAsync(string materialId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(materialId);
        var query = new Dictionary<string, object?> { ["material_ids"] = materialId };
        var res = await _http.GetAsync<SimilarityData>("materials/similarity/", query, cancellationToken).ConfigureAwait(false);
        return res.Data;
    }

    public async Task<IReadOnlyList<SubstrateMatchData>> GetSubstratesAsync(string materialId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(materialId);
        var query = new Dictionary<string, object?> { ["film_id"] = materialId };
        var res = await _http.GetAsync<SubstrateMatchData>("materials/substrates/", query, cancellationToken).ConfigureAwait(false);
        return res.Data;
    }

    public async Task<IReadOnlyList<GrainBoundaryData>> GetGrainBoundariesAsync(string materialId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(materialId);
        var query = new Dictionary<string, object?> { ["material_ids"] = materialId };
        var res = await _http.GetAsync<GrainBoundaryData>("materials/grain_boundaries/", query, cancellationToken).ConfigureAwait(false);
        return res.Data;
    }

    public async Task<ProvenanceData?> GetProvenanceAsync(string materialId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(materialId);
        var query = new Dictionary<string, object?> { ["material_ids"] = materialId, ["_limit"] = 1 };
        var res = await _http.GetAsync<ProvenanceData>("materials/provenance/", query, cancellationToken).ConfigureAwait(false);
        return res.Data.FirstOrDefault();
    }
}
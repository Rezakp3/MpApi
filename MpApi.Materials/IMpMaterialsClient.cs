using MpApi.Materials.Models;

namespace MpApi.Materials;

/// <summary>
/// Client interface for interacting with Materials Project materials endpoints.
/// </summary>
public interface IMpMaterialsClient
{
    /// <summary>
    /// Gets the summary data for a specific material by its MP ID (e.g. "mp-149" for Silicon).
    /// </summary>
    Task<MaterialSummary?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Performs a single-page search based on the provided search criteria.
    /// </summary>
    Task<IReadOnlyList<MaterialSummary>> SearchAsync(SummarySearchCriteria criteria, CancellationToken cancellationToken = default);

    /// <summary>
    /// Streams all matching materials asynchronously across all pages without memory bloat.
    /// </summary>
    IAsyncEnumerable<MaterialSummary> StreamSearchAsync(
        SummarySearchCriteria criteria,
        int pageSize = 100,
        CancellationToken cancellationToken = default);
    Task<ElasticityData?> GetElasticityAsync(string materialId, CancellationToken cancellationToken = default);
    Task<DielectricData?> GetDielectricAsync(string materialId, CancellationToken cancellationToken = default);
    Task<PiezoelectricData?> GetPiezoelectricAsync(string materialId, CancellationToken cancellationToken = default);
    Task<SurfacePropertyData?> GetSurfacePropertiesAsync(string materialId, CancellationToken cancellationToken = default);
    Task<CrystalStructure?> GetStructureAsync(string materialId, CancellationToken cancellationToken = default);

    Task<ElectronicStructureData?> GetElectronicStructureAsync(string materialId, CancellationToken cancellationToken = default);
    // --- Spectroscopy & XAS ---
    Task<IReadOnlyList<XasData>> GetXasAsync(string materialId, string? absorbingElement = null, string? edge = null, CancellationToken cancellationToken = default);

    // --- Structural Similarity ---
    Task<IReadOnlyList<SimilarityData>> GetSimilarMaterialsAsync(string materialId, CancellationToken cancellationToken = default);

    // --- Substrates & Epitaxy ---
    Task<IReadOnlyList<SubstrateMatchData>> GetSubstratesAsync(string materialId, CancellationToken cancellationToken = default);

    // --- Grain Boundaries ---
    Task<IReadOnlyList<GrainBoundaryData>> GetGrainBoundariesAsync(string materialId, CancellationToken cancellationToken = default);

    // --- Provenance & History ---
    Task<ProvenanceData?> GetProvenanceAsync(string materialId, CancellationToken cancellationToken = default);
}
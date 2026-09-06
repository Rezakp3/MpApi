using MpApi.Common.Models;
using MpApi.Domains.Materials.Summary.Models;

namespace MpApi.Domains.Materials.Summary;

/// <summary>
/// Client contract for querying the Materials Project Summary endpoint.
/// </summary>
public interface ISummaryClient
{
    /// <summary>
    /// Retrieves summary data for a single material by its Materials Project ID.
    /// </summary>
    /// <param name="materialId">Material identifier (e.g., "mp-149").</param>
    /// <param name="fields">Optional projection fields to limit response payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Summary document of the material, or null if not found.</returns>
    Task<SummaryDoc?> GetByIdAsync(
        string materialId,
        IEnumerable<string>? fields = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches materials summary records matching the provided filter criteria.
    /// </summary>
    /// <param name="filter">Search and filter query parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Envelope response containing the matched summary documents and metadata.</returns>
    Task<MpResponse<IReadOnlyList<SummaryDoc>>> SearchAsync(
        SummarySearchFilter filter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Convenience method to search materials by chemical formula.
    /// </summary>
    /// <param name="formula">Chemical formula (e.g., "Fe2O3", "Si").</param>
    /// <param name="limit">Maximum number of records to return.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<IReadOnlyList<SummaryDoc>> SearchByFormulaAsync(
        string formula,
        int limit = 10,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Convenience method to search materials within a chemical system.
    /// </summary>
    /// <param name="chemsys">Chemical system string (e.g., "Li-Fe-O").</param>
    /// <param name="limit">Maximum number of records to return.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<IReadOnlyList<SummaryDoc>> SearchByChemsysAsync(
        string chemsys,
        int limit = 50,
        CancellationToken cancellationToken = default);
}
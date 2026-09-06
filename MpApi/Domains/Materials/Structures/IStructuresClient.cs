using MpApi.Common.Models;
using MpApi.Domains.Materials.Structures.Models;

namespace MpApi.Domains.Materials.Structures;

/// <summary>
/// Client contract for accessing crystal structures, unit cell lattices, and atomic sites.
/// </summary>
public interface IStructuresClient
{
    /// <summary>
    /// Gets the crystal structure document for a specific material identifier.
    /// </summary>
    /// <param name="materialId">Materials Project ID (e.g., "mp-149").</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<MaterialStructureDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches crystal structures matching specified filter conditions.
    /// </summary>
    Task<MpResponse<IReadOnlyList<MaterialStructureDoc>>> SearchAsync(
        StructureSearchFilter filter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the raw Crystallographic Information File (CIF) text for a material.
    /// </summary>
    /// <param name="materialId">Materials Project ID (e.g., "mp-149").</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Raw CIF text string.</returns>
    Task<string> GetCifAsync(string materialId, CancellationToken cancellationToken = default);
}
using MpApi.Common.Models;
using MpApi.Domains.Thermodynamics.Thermo.Models;

namespace MpApi.Domains.Thermodynamics.Thermo;

/// <summary>
/// Client contract for querying thermodynamic data, formation energies, and phase balance data.
/// </summary>
public interface IThermoClient
{
    /// <summary>
    /// Gets thermodynamic properties for a single material by its identifier.
    /// </summary>
    Task<ThermoDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches thermodynamic entries matching specified criteria.
    /// </summary>
    Task<MpResponse<IReadOnlyList<ThermoDoc>>> SearchAsync(
        ThermoSearchFilter filter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all thermodynamic entries within a chemical system required to compute and plot Phase Diagrams.
    /// </summary>
    /// <param name="chemsys">Chemical system (e.g. "Li-Fe-O" or "Fe-O").</param>
    /// <param name="thermoType">Thermodynamic functional type (e.g. "GGA_GGA+U", "R2SCAN").</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<IReadOnlyList<ThermoDoc>> GetPhaseDiagramEntriesAsync(
        string chemsys,
        string? thermoType = null,
        CancellationToken cancellationToken = default);
}
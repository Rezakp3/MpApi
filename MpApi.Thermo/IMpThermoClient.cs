using MpApi.Thermo.Models;

namespace MpApi.Thermo;

/// <summary>
/// Client contract for Materials Project thermodynamics endpoints.
/// </summary>
public interface IMpThermoClient
{
    Task<ThermoData?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ThermoData>> SearchAsync(ThermoSearchCriteria criteria, CancellationToken cancellationToken = default);
    IAsyncEnumerable<ThermoData> StreamSearchAsync(ThermoSearchCriteria criteria, int pageSize = 100, CancellationToken cancellationToken = default);
    Task<PhaseDiagramData?> GetPhaseDiagramAsync(string chemsys, CancellationToken cancellationToken = default);
}

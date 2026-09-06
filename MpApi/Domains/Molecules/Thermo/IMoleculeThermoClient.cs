using MpApi.Common.Models;
using MpApi.Domains.Molecules.Thermo.Models;

namespace MpApi.Domains.Molecules.Thermo;

public interface IMoleculeThermoClient
{
    Task<MoleculeThermoDoc?> GetByIdAsync(string moleculeId, CancellationToken cancellationToken = default);
    Task<MpResponse<IReadOnlyList<MoleculeThermoDoc>>> SearchAsync(string? formula = null, int limit = 20, CancellationToken cancellationToken = default);
}

using MpApi.Common.Models;
using MpApi.Domains.Molecules.Summary.Models;

namespace MpApi.Domains.Molecules.Summary;

public interface IMoleculeSummaryClient
{
    Task<MoleculeSummaryDoc?> GetByIdAsync(string moleculeId, CancellationToken cancellationToken = default);
    Task<MpResponse<IReadOnlyList<MoleculeSummaryDoc>>> SearchAsync(MoleculeSummarySearchFilter filter, CancellationToken cancellationToken = default);
}

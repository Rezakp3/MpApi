using MpApi.Common.Models;
using MpApi.Domains.Applications.Synthesis.Models;

namespace MpApi.Domains.Applications.Synthesis;

public interface ISynthesisClient
{
    Task<MpResponse<IReadOnlyList<SynthesisDoc>>> SearchByTargetFormulaAsync(string targetFormula, int limit = 20, CancellationToken cancellationToken = default);
}

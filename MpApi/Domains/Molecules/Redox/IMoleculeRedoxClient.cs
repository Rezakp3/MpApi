using MpApi.Common.Models;
using MpApi.Domains.Molecules.Redox.Models;

namespace MpApi.Domains.Molecules.Redox;

public interface IMoleculeRedoxClient
{
    Task<MpResponse<IReadOnlyList<MoleculeRedoxDoc>>> SearchAsync(int limit = 50, CancellationToken cancellationToken = default);
}

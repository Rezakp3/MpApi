using MpApi.Common.Models;
using MpApi.Domains.Applications.Substrates.Models;

namespace MpApi.Domains.Applications.Substrates;

public interface ISubstratesClient
{
    Task<MpResponse<IReadOnlyList<SubstrateDoc>>> SearchAsync(SubstrateSearchFilter filter, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SubstrateDoc>> GetMatchesForMaterialAsync(string materialId, CancellationToken cancellationToken = default);
}

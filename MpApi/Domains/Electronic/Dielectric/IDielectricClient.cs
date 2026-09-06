using MpApi.Common.Models;
using MpApi.Domains.Electronic.Dielectric.Models;

namespace MpApi.Domains.Electronic.Dielectric;

public interface IDielectricClient
{
    Task<DielectricDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default);
    Task<MpResponse<IReadOnlyList<DielectricDoc>>> SearchAsync(DielectricSearchFilter filter, CancellationToken cancellationToken = default);
}

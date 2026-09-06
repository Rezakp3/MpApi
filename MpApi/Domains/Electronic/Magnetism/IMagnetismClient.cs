using MpApi.Common.Models;
using MpApi.Domains.Electronic.Magnetism.Models;

namespace MpApi.Domains.Electronic.Magnetism;

public interface IMagnetismClient
{
    Task<MagnetismDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default);
    Task<MpResponse<IReadOnlyList<MagnetismDoc>>> SearchAsync(MagnetismSearchFilter filter, CancellationToken cancellationToken = default);
}

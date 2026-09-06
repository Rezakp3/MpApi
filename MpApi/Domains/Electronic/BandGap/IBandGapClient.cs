using MpApi.Common.Models;
using MpApi.Domains.Electronic.BandGap.Models;

namespace MpApi.Domains.Electronic.BandGap;

/// <summary>
/// Client contract for querying accurate band gaps calculated via various functional approximations (e.g. HSE06, GGA).
/// </summary>
public interface IBandGapClient
{
    Task<BandGapDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default);
    Task<MpResponse<IReadOnlyList<BandGapDoc>>> SearchAsync(BandGapSearchFilter filter, CancellationToken cancellationToken = default);
}

using MpApi.Common.Models;
using MpApi.Domains.Applications.Alloys.Models;

namespace MpApi.Domains.Applications.Alloys;

public interface IAlloysClient
{
    Task<MpResponse<IReadOnlyList<AlloyDoc>>> SearchAsync(string? formula = null, int limit = 50, CancellationToken cancellationToken = default);
}

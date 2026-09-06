using MpApi.Common.Models;
using MpApi.Domains.Applications.Batteries.Models;

namespace MpApi.Domains.Applications.Batteries;

public interface IBatteriesClient
{
    Task<BatteryDoc?> GetByIdAsync(string batteryId, CancellationToken cancellationToken = default);
    Task<MpResponse<IReadOnlyList<BatteryDoc>>> SearchAsync(BatterySearchFilter filter, CancellationToken cancellationToken = default);
}

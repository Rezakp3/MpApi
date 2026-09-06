using MpApi.Common.Models;
using MpApi.Domains.Mechanical.Piezoelectric.Models;

namespace MpApi.Domains.Mechanical.Piezoelectric;

public interface IPiezoelectricClient
{
    Task<PiezoelectricDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default);
    Task<MpResponse<IReadOnlyList<PiezoelectricDoc>>> SearchAsync(PiezoelectricSearchFilter filter, CancellationToken cancellationToken = default);
}

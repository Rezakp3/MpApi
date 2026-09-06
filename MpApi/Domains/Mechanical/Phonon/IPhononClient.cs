using MpApi.Common.Models;
using MpApi.Domains.Mechanical.Phonon.Models;

namespace MpApi.Domains.Mechanical.Phonon;

public interface IPhononClient
{
    Task<PhononDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default);
    Task<MpResponse<IReadOnlyList<PhononDoc>>> SearchAsync(PhononSearchFilter filter, CancellationToken cancellationToken = default);
}

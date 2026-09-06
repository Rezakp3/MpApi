using MpApi.Common.Models;
using MpApi.Domains.Electronic.ElectronicStructure.Models;

namespace MpApi.Domains.Electronic.ElectronicStructure;

public interface IElectronicStructureClient
{
    Task<ElectronicStructureDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default);
    Task<MpResponse<IReadOnlyList<ElectronicStructureDoc>>> SearchAsync(ElectronicStructureSearchFilter filter, CancellationToken cancellationToken = default);
}

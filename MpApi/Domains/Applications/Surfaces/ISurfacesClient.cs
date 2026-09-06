using MpApi.Common.Models;

namespace MpApi.Domains.Applications.Surfaces;

public interface ISurfacesClient
{
    Task<SurfaceDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default);
    Task<MpResponse<IReadOnlyList<SurfaceDoc>>> SearchAsync(string? chemsys = null, int limit = 50, CancellationToken cancellationToken = default);
}

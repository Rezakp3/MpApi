using MpApi.Domains.Materials.Robocrys.Models;

namespace MpApi.Domains.Materials.Robocrys;

public interface IRobocrysClient
{
    Task<RobocrysDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default);
}

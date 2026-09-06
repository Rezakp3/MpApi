using MpApi.Common.Models;
using MpApi.Domains.Materials.Tasks.Models;

namespace MpApi.Domains.Materials.Tasks;

public interface ITasksClient
{
    Task<MaterialTaskDoc?> GetByIdAsync(string taskId, CancellationToken cancellationToken = default);
    Task<MpResponse<IReadOnlyList<MaterialTaskDoc>>> SearchAsync(string? taskType = null, int limit = 20, CancellationToken cancellationToken = default);
}

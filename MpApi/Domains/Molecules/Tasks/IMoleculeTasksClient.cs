using MpApi.Domains.Molecules.Tasks.Models;

namespace MpApi.Domains.Molecules.Tasks;

public interface IMoleculeTasksClient
{
    Task<MoleculeTaskDoc?> GetByIdAsync(string taskId, CancellationToken cancellationToken = default);
}

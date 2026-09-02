using MpApi.Molecules.Models;

namespace MpApi.Molecules;

public interface IMpMoleculesClient
{
    Task<MoleculeSummary?> GetByIdAsync(string moleculeId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MoleculeSummary>> SearchBySmilesAsync(string smiles, CancellationToken cancellationToken = default);
    IAsyncEnumerable<MoleculeSummary> StreamSearchAsync(Dictionary<string, object?> queryParams, CancellationToken cancellationToken = default);
}

using MpApi.Common.Http;
using MpApi.Domains.Molecules.Tasks.Models;

namespace MpApi.Domains.Molecules.Tasks;

public sealed class MoleculeTasksClient : IMoleculeTasksClient
{
    private const string BaseEndpoint = "molecules/tasks/";
    private readonly IMpHttpClient _httpClient;

    public MoleculeTasksClient(IMpHttpClient httpClient) => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<MoleculeTaskDoc?> GetByIdAsync(string taskId, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?> { ["task_ids"] = taskId };
        var response = await _httpClient.GetAsync<IReadOnlyList<MoleculeTaskDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }
}
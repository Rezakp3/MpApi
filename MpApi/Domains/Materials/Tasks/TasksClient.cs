using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Materials.Tasks.Models;

namespace MpApi.Domains.Materials.Tasks;

public sealed class TasksClient : ITasksClient
{
    private const string BaseEndpoint = "materials/tasks/";
    private readonly IMpHttpClient _httpClient;

    public TasksClient(IMpHttpClient httpClient) => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<MaterialTaskDoc?> GetByIdAsync(string taskId, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?> { ["task_ids"] = taskId };
        var response = await _httpClient.GetAsync<IReadOnlyList<MaterialTaskDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }

    public async Task<MpResponse<IReadOnlyList<MaterialTaskDoc>>> SearchAsync(string? taskType = null, int limit = 20, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?> { ["_limit"] = limit.ToString() };
        if (!string.IsNullOrWhiteSpace(taskType)) query["task_type"] = taskType;
        return await _httpClient.GetAsync<IReadOnlyList<MaterialTaskDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
    }
}
using System.Text.Json.Serialization;

namespace MpApi.Domains.Materials.Tasks.Models;

public record MaterialTaskDoc
{
    [JsonPropertyName("task_id")]
    public string TaskId { get; init; } = string.Empty;

    [JsonPropertyName("task_type")]
    public string? TaskType { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }

    [JsonPropertyName("completed_at")]
    public DateTime? CompletedAt { get; init; }
}

using System.Text.Json.Serialization;
using MpApi.Common.Models;

namespace MpApi.Domains.Molecules.Tasks.Models;

public record MoleculeTaskDoc
{
    [JsonPropertyName("task_id")]
    public string TaskId { get; init; } = string.Empty;

    [JsonPropertyName("molecule_id")]
    public string MoleculeId { get; init; } = string.Empty;

    [JsonPropertyName("level_of_theory")]
    public string? LevelOfTheory { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }
}

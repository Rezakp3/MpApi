using System.Text.Json.Serialization;

namespace MpApi.Domains.Applications.Synthesis.Models;

public record SynthesisDoc
{
    [JsonPropertyName("synthesis_id")]
    public string SynthesisId { get; init; } = string.Empty;

    [JsonPropertyName("synthesis_type")]
    public string? SynthesisType { get; init; }

    [JsonPropertyName("targets")]
    public IReadOnlyList<string>? Targets { get; init; }

    [JsonPropertyName("precursors")]
    public IReadOnlyList<string>? Precursors { get; init; }

    [JsonPropertyName("doi")]
    public string? Doi { get; init; }

    [JsonPropertyName("paragraph_string")]
    public string? RecipeParagraph { get; init; }
}

using System.Text.Json.Serialization;

namespace MpApi.Synthesis.Models;

/// <summary>
/// Experimental synthesis recipe mined from scientific literature.
/// </summary>
public sealed record SynthesisRecipe
{
    [JsonPropertyName("synthesis_id")]
    public required string SynthesisId { get; init; }

    [JsonPropertyName("doi")]
    public string? Doi { get; init; }

    [JsonPropertyName("paragraph_string")]
    public string? OriginalText { get; init; }

    [JsonPropertyName("synthesis_type")]
    public string? SynthesisType { get; init; } // e.g. "solid-state", "sol-gel"

    [JsonPropertyName("precursors")]
    public IReadOnlyList<string> Precursors { get; init; } = [];

    [JsonPropertyName("targets")]
    public IReadOnlyList<string> TargetMaterials { get; init; } = [];
}
using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Provenance, author information, and DFT calculation references for a material.
/// </summary>
public sealed record ProvenanceData
{
    [JsonPropertyName("material_id")]
    public required string MaterialId { get; init; }

    [JsonPropertyName("authors")]
    public IReadOnlyList<string> Authors { get; init; } = [];

    [JsonPropertyName("references")]
    public IReadOnlyList<string> References { get; init; } = [];

    [JsonPropertyName("remarks")]
    public IReadOnlyList<string> Remarks { get; init; } = [];
}
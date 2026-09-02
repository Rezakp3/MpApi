using System.Text.Json.Serialization;

namespace MpApi.Thermo.Models;

/// <summary>
/// Represents a calculated chemical phase diagram (Convex Hull).
/// </summary>
public sealed record PhaseDiagramData
{
    [JsonPropertyName("chemsys")]
    public required string Chemsys { get; init; }

    [JsonPropertyName("elements")]
    public IReadOnlyList<string> Elements { get; init; } = [];

    [JsonPropertyName("entries")]
    public IReadOnlyList<PhaseDiagramEntry> StableEntries { get; init; } = [];

    [JsonPropertyName("all_entries")]
    public IReadOnlyList<PhaseDiagramEntry> AllEntries { get; init; } = [];

    [JsonPropertyName("dim")]
    public int? Dimension { get; init; }

    [JsonPropertyName("thermo_type")]
    public string? ThermoType { get; init; }
}

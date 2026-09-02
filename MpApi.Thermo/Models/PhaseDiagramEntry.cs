using System.Text.Json.Serialization;

namespace MpApi.Thermo.Models;

/// <summary>
/// Represents a candidate or stable phase entry on the convex hull.
/// </summary>
public sealed record PhaseDiagramEntry
{
    [JsonPropertyName("entry_id")]
    public string? EntryId { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("energy")]
    public double Energy { get; init; }

    [JsonPropertyName("energy_per_atom")]
    public double? EnergyPerAtom { get; init; }

    [JsonPropertyName("composition")]
    public IReadOnlyDictionary<string, double> Composition { get; init; } = new Dictionary<string, double>();
}
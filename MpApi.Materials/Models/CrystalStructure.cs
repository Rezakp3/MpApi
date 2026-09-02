using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Full 3D periodic crystal structure definition.
/// </summary>
public sealed record CrystalStructure
{
    [JsonPropertyName("lattice")]
    public required LatticeData Lattice { get; init; }

    [JsonPropertyName("sites")]
    public IReadOnlyList<PeriodicSiteData> Sites { get; init; } = [];

    [JsonPropertyName("charge")]
    public double? Charge { get; init; }
}
using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Represents crystallographic symmetry properties.
/// </summary>
public sealed record SymmetryData
{
    [JsonPropertyName("crystal_system")]
    public CrystalSystem? CrystalSystem { get; init; }

    [JsonPropertyName("symbol")]
    public string? Symbol { get; init; }

    [JsonPropertyName("number")]
    public int? Number { get; init; }

    [JsonPropertyName("point_group")]
    public string? PointGroup { get; init; }
}
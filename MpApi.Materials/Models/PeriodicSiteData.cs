using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Represents an atomic site with its fractional and Cartesian coordinates.
/// </summary>
public sealed record PeriodicSiteData
{
    [JsonPropertyName("species")]
    public IReadOnlyList<SpeciesElementData> Species { get; init; } = [];

    [JsonPropertyName("abc")]
    public IReadOnlyList<double> FractionalCoordinates { get; init; } = [];

    [JsonPropertyName("xyz")]
    public IReadOnlyList<double> CartesianCoordinates { get; init; } = [];

    [JsonPropertyName("label")]
    public string? Label { get; init; }
}

public sealed record SpeciesElementData
{
    [JsonPropertyName("element")]
    public required string Element { get; init; }

    [JsonPropertyName("occu")]
    public double Occupancy { get; init; } = 1.0;
}
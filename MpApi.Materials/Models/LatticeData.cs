using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Represents the unit cell lattice parameters and 3x3 transformation matrix.
/// </summary>
public sealed record LatticeData
{
    [JsonPropertyName("matrix")]
    public IReadOnlyList<IReadOnlyList<double>> Matrix { get; init; } = [];

    [JsonPropertyName("a")]
    public double A { get; init; }

    [JsonPropertyName("b")]
    public double B { get; init; }

    [JsonPropertyName("c")]
    public double C { get; init; }

    [JsonPropertyName("alpha")]
    public double Alpha { get; init; }

    [JsonPropertyName("beta")]
    public double Beta { get; init; }

    [JsonPropertyName("gamma")]
    public double Gamma { get; init; }

    [JsonPropertyName("volume")]
    public double Volume { get; init; }
}
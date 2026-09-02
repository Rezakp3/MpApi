using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Surface energies, Miller indices, and Wulff shape properties.
/// </summary>
public sealed record SurfacePropertyData
{
    [JsonPropertyName("material_id")]
    public required string MaterialId { get; init; }

    [JsonPropertyName("surface_energy")]
    public double? WeightedSurfaceEnergy { get; init; } // J/m^2

    [JsonPropertyName("shape_factor")]
    public double? ShapeFactor { get; init; }

    [JsonPropertyName("has_reconstructed")]
    public bool? HasReconstructedSurfaces { get; init; }
}
using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Dielectric constants and optical refractive properties.
/// </summary>
public sealed record DielectricData
{
    [JsonPropertyName("material_id")]
    public required string MaterialId { get; init; }

    [JsonPropertyName("e_total")]
    public double? DielectricTotal { get; init; } // Total dielectric constant

    [JsonPropertyName("e_ionic")]
    public double? DielectricIonic { get; init; }

    [JsonPropertyName("e_electronic")]
    public double? DielectricElectronic { get; init; }

    [JsonPropertyName("n")]
    public double? RefractiveIndex { get; init; } // Optical refractive index
}
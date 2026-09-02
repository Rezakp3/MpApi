using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Detailed electronic band structure and density of states (DOS) data.
/// </summary>
public sealed record ElectronicStructureData
{
    [JsonPropertyName("material_id")]
    public required string MaterialId { get; init; }

    [JsonPropertyName("band_gap")]
    public double? BandGap { get; init; }

    [JsonPropertyName("is_gap_direct")]
    public bool? IsGapDirect { get; init; }

    [JsonPropertyName("is_metal")]
    public bool? IsMetal { get; init; }

    [JsonPropertyName("cbm")]
    public double? ConductionBandMinimum { get; init; }

    [JsonPropertyName("vbm")]
    public double? ValenceBandMaximum { get; init; }

    [JsonPropertyName("efermi")]
    public double? FermiEnergy { get; init; }
}
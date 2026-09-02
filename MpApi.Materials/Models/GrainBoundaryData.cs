using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Grain boundary energies and crystallographic boundary parameters.
/// </summary>
public sealed record GrainBoundaryData
{
    [JsonPropertyName("material_id")]
    public required string MaterialId { get; init; }

    [JsonPropertyName("sigma")]
    public int? Sigma { get; init; }

    [JsonPropertyName("gb_energy")]
    public double? GrainBoundaryEnergy { get; init; } // J/m^2

    [JsonPropertyName("rotation_axis")]
    public IReadOnlyList<int> RotationAxis { get; init; } = [];

    [JsonPropertyName("rotation_angle")]
    public double? RotationAngle { get; init; }
}
using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Piezoelectric tensor and electromechanical properties.
/// </summary>
public sealed record PiezoelectricData
{
    [JsonPropertyName("material_id")]
    public required string MaterialId { get; init; }

    [JsonPropertyName("e_ij_max")]
    public double? MaxPiezoelectricTensorComponent { get; init; }

    [JsonPropertyName("piezoelectric_tensor")]
    public IReadOnlyList<IReadOnlyList<double>>? PiezoelectricTensor { get; init; } // 3x6 tensor
}
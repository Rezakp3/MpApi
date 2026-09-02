using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Represents crystal structure similarity matching results.
/// </summary>
public sealed record SimilarityData
{
    [JsonPropertyName("material_id")]
    public required string MaterialId { get; init; }

    [JsonPropertyName("similar_material_id")]
    public string? SimilarMaterialId { get; init; }

    [JsonPropertyName("similarity_metric")]
    public double? SimilarityScore { get; init; }
}
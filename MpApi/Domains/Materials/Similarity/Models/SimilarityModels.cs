using System.Text.Json.Serialization;
using MpApi.Common.Models;

namespace MpApi.Domains.Materials.Similarity.Models;

public record SimilarityDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("similar_materials")]
    public IReadOnlyList<SimilarMaterialMatch>? SimilarMaterials { get; init; }
}

public record SimilarMaterialMatch
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("similarity_score")]
    public double SimilarityScore { get; init; }
}

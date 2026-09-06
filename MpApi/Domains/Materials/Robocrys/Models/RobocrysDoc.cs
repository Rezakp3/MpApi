using System.Text.Json.Serialization;

namespace MpApi.Domains.Materials.Robocrys.Models;

public record RobocrysDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("condensed_structure")]
    public string? CondensedStructure { get; init; }
}

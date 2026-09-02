using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

public sealed record MaterialCoreData
{
    [JsonPropertyName("material_id")]
    public required string MaterialId { get; init; }

    [JsonPropertyName("structure")]
    public CrystalStructure? Structure { get; init; }

    [JsonPropertyName("deprecated")]
    public bool? Deprecated { get; init; }
}
using System.Text.Json.Serialization;

namespace MpApi.Domains.Materials.Xas.Models;

public record XasDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("edge")]
    public string? Edge { get; init; }

    [JsonPropertyName("absorbing_element")]
    public string? AbsorbingElement { get; init; }

    [JsonPropertyName("spectrum")]
    public IReadOnlyList<IReadOnlyList<double>>? Spectrum { get; init; }
}

using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

public sealed record SpeciesElementData
{
    [JsonPropertyName("element")]
    public string Element { get; init; } = string.Empty;

    [JsonPropertyName("occu")]
    public double Occupancy { get; init; } = 1.0;
}
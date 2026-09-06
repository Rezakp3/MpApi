using System.Text.Json.Serialization;
using MpApi.Common.Models;

namespace MpApi.Domains.Materials.Chemenv;

public record ChemenvDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("valences")]
    public IReadOnlyList<int>? Valences { get; init; }

    [JsonPropertyName("coordination_environments")]
    public IReadOnlyList<SiteEnvironmentData>? CoordinationEnvironments { get; init; }
}

public record SiteEnvironmentData
{
    [JsonPropertyName("site_index")]
    public int SiteIndex { get; init; }

    [JsonPropertyName("coordination_number")]
    public int CoordinationNumber { get; init; }

    [JsonPropertyName("symbol")]
    public string? Symbol { get; init; }

    [JsonPropertyName("csm")]
    public double? ContinuousSymmetryMeasure { get; init; }
}

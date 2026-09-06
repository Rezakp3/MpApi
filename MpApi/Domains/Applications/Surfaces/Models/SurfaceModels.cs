using System.Text.Json.Serialization;

namespace MpApi.Domains.Applications.Surfaces;

public record SurfaceDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("formula_pretty")]
    public string? FormulaPretty { get; init; }

    [JsonPropertyName("surface_energy")]
    public double? SurfaceEnergy { get; init; }

    [JsonPropertyName("surface_anisotropy")]
    public double? SurfaceAnisotropy { get; init; }

    [JsonPropertyName("surfaces")]
    public IReadOnlyList<SingleSurfaceData>? Surfaces { get; init; }
}

public record SingleSurfaceData
{
    [JsonPropertyName("miller_index")]
    public IReadOnlyList<int>? MillerIndex { get; init; }

    [JsonPropertyName("surface_energy")]
    public double? SurfaceEnergy { get; init; }

    [JsonPropertyName("area_fraction")]
    public double? AreaFraction { get; init; }
}

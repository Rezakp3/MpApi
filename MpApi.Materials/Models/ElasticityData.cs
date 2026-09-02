using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Mechanical and elastic properties (elastic tensor, bulk and shear moduli).
/// </summary>
public sealed record ElasticityData
{
    [JsonPropertyName("material_id")]
    public required string MaterialId { get; init; }

    [JsonPropertyName("k_vrh")]
    public double? BulkModulusVrh { get; init; } // Voigt-Reuss-Hill bulk modulus (GPa)

    [JsonPropertyName("g_vrh")]
    public double? ShearModulusVrh { get; init; } // Shear modulus (GPa)

    [JsonPropertyName("universal_anisotropy")]
    public double? UniversalAnisotropy { get; init; }

    [JsonPropertyName("homogeneous_poisson")]
    public double? PoissonRatio { get; init; }

    [JsonPropertyName("elastic_tensor")]
    public IReadOnlyList<IReadOnlyList<double>>? ElasticTensor { get; init; } // 6x6 Voigt matrix
}
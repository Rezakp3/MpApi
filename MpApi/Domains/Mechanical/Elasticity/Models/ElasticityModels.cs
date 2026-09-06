using System.Text.Json.Serialization;

namespace MpApi.Domains.Mechanical.Elasticity.Models;

/// <summary>
/// Represents mechanical elasticity properties, stiffness tensors, and elastic moduli.
/// </summary>
public record ElasticityDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("formula_pretty")]
    public string? FormulaPretty { get; init; }

    /// <summary>
    /// Voigt bulk modulus in GPa.
    /// </summary>
    [JsonPropertyName("k_voigt")]
    public double? KVoigt { get; init; }

    /// <summary>
    /// Reuss bulk modulus in GPa.
    /// </summary>
    [JsonPropertyName("k_reuss")]
    public double? KReuss { get; init; }

    /// <summary>
    /// Voigt-Reuss-Hill (VRH) average bulk modulus in GPa.
    /// </summary>
    [JsonPropertyName("k_vrh")]
    public double? KVrh { get; init; }

    /// <summary>
    /// Voigt shear modulus in GPa.
    /// </summary>
    [JsonPropertyName("g_voigt")]
    public double? GVoigt { get; init; }

    /// <summary>
    /// Reuss shear modulus in GPa.
    /// </summary>
    [JsonPropertyName("g_reuss")]
    public double? GReuss { get; init; }

    /// <summary>
    /// Voigt-Reuss-Hill (VRH) average shear modulus in GPa.
    /// </summary>
    [JsonPropertyName("g_vrh")]
    public double? GVrh { get; init; }

    /// <summary>
    /// Universal elastic anisotropy index.
    /// </summary>
    [JsonPropertyName("universal_anisotropy")]
    public double? UniversalAnisotropy { get; init; }

    /// <summary>
    /// Homogeneous Poisson's ratio.
    /// </summary>
    [JsonPropertyName("homogeneous_poisson")]
    public double? HomogeneousPoisson { get; init; }

    /// <summary>
    /// 6x6 elastic stiffness tensor (C_ij in GPa).
    /// </summary>
    [JsonPropertyName("elastic_tensor")]
    public IReadOnlyList<IReadOnlyList<double>>? ElasticTensor { get; init; }

    /// <summary>
    /// 6x6 elastic compliance tensor (S_ij in 1/GPa).
    /// </summary>
    [JsonPropertyName("compliance_tensor")]
    public IReadOnlyList<IReadOnlyList<double>>? ComplianceTensor { get; init; }

    /// <summary>
    /// Indicates whether the material is mechanically/elastically stable.
    /// </summary>
    [JsonPropertyName("is_elastic_stable")]
    public bool? IsElasticStable { get; init; }
}

public record ElasticitySearchFilter
{
    public string? MaterialId { get; init; }
    public string? Formula { get; init; }
    public string? Chemsys { get; init; }
    public double? KVrhMin { get; init; }
    public double? KVrhMax { get; init; }
    public double? GVrhMin { get; init; }
    public double? GVrhMax { get; init; }
    public double? PoissonMin { get; init; }
    public double? PoissonMax { get; init; }
    public bool? IsElasticStable { get; init; }
    public IReadOnlyList<string>? Fields { get; init; }
    public int? Limit { get; init; }
    public int? Skip { get; init; }

    public IDictionary<string, string?> ToQueryParameters()
    {
        var dict = new Dictionary<string, string?>();

        if (!string.IsNullOrWhiteSpace(MaterialId)) dict["material_ids"] = MaterialId;
        if (!string.IsNullOrWhiteSpace(Formula)) dict["formula"] = Formula;
        if (!string.IsNullOrWhiteSpace(Chemsys)) dict["chemsys"] = Chemsys;

        if (KVrhMin.HasValue) dict["k_vrh_min"] = KVrhMin.Value.ToString();
        if (KVrhMax.HasValue) dict["k_vrh_max"] = KVrhMax.Value.ToString();
        if (GVrhMin.HasValue) dict["g_vrh_min"] = GVrhMin.Value.ToString();
        if (GVrhMax.HasValue) dict["g_vrh_max"] = GVrhMax.Value.ToString();
        if (PoissonMin.HasValue) dict["homogeneous_poisson_min"] = PoissonMin.Value.ToString();
        if (PoissonMax.HasValue) dict["homogeneous_poisson_max"] = PoissonMax.Value.ToString();
        if (IsElasticStable.HasValue) dict["is_elastic_stable"] = IsElasticStable.Value.ToString().ToLowerInvariant();

        if (Fields?.Count > 0) dict["_fields"] = string.Join(",", Fields);
        if (Limit.HasValue) dict["_limit"] = Limit.Value.ToString();
        if (Skip.HasValue) dict["_skip"] = Skip.Value.ToString();

        return dict;
    }
}
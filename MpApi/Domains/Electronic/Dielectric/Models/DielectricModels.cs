using System.Text.Json.Serialization;

namespace MpApi.Domains.Electronic.Dielectric.Models;

/// <summary>
/// Represents calculated dielectric tensors (ionic and electronic contributions) and refractive indices.
/// </summary>
public record DielectricDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("formula_pretty")]
    public string? FormulaPretty { get; init; }

    /// <summary>
    /// Total dielectric tensor (3x3 matrix).
    /// </summary>
    [JsonPropertyName("total")]
    public IReadOnlyList<IReadOnlyList<double>>? Total { get; init; }

    /// <summary>
    /// Electronic contribution to the dielectric tensor (3x3 matrix).
    /// </summary>
    [JsonPropertyName("electronic")]
    public IReadOnlyList<IReadOnlyList<double>>? Electronic { get; init; }

    /// <summary>
    /// Ionic contribution to the dielectric tensor (3x3 matrix).
    /// </summary>
    [JsonPropertyName("ionic")]
    public IReadOnlyList<IReadOnlyList<double>>? Ionic { get; init; }

    /// <summary>
    /// Calculated optical refractive index (n).
    /// </summary>
    [JsonPropertyName("n")]
    public double? RefractiveIndex { get; init; }

    /// <summary>
    /// Average / isotropic dielectric constant (e_total).
    /// </summary>
    [JsonPropertyName("e_total")]
    public double? ETotal { get; init; }

    [JsonPropertyName("e_electronic")]
    public double? EElectronic { get; init; }

    [JsonPropertyName("e_ionic")]
    public double? EIonic { get; init; }
}

public record DielectricSearchFilter
{
    public string? MaterialId { get; init; }
    public string? Formula { get; init; }
    public string? Chemsys { get; init; }
    public double? TotalMin { get; init; }
    public double? TotalMax { get; init; }
    public double? RefractiveIndexMin { get; init; }
    public double? RefractiveIndexMax { get; init; }
    public IReadOnlyList<string>? Fields { get; init; }
    public int? Limit { get; init; }
    public int? Skip { get; init; }

    public IDictionary<string, string?> ToQueryParameters()
    {
        var dict = new Dictionary<string, string?>();

        if (!string.IsNullOrWhiteSpace(MaterialId)) dict["material_ids"] = MaterialId;
        if (!string.IsNullOrWhiteSpace(Formula)) dict["formula"] = Formula;
        if (!string.IsNullOrWhiteSpace(Chemsys)) dict["chemsys"] = Chemsys;

        if (TotalMin.HasValue) dict["total_min"] = TotalMin.Value.ToString();
        if (TotalMax.HasValue) dict["total_max"] = TotalMax.Value.ToString();
        if (RefractiveIndexMin.HasValue) dict["n_min"] = RefractiveIndexMin.Value.ToString();
        if (RefractiveIndexMax.HasValue) dict["n_max"] = RefractiveIndexMax.Value.ToString();

        if (Fields?.Count > 0) dict["_fields"] = string.Join(",", Fields);
        if (Limit.HasValue) dict["_limit"] = Limit.Value.ToString();
        if (Skip.HasValue) dict["_skip"] = Skip.Value.ToString();

        return dict;
    }
}
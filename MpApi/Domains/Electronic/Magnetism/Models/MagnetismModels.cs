using System.Text.Json.Serialization;

namespace MpApi.Domains.Electronic.Magnetism.Models;

/// <summary>
/// Represents magnetic characteristics including ordering type, total magnetization, and site moments.
/// </summary>
public record MagnetismDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("formula_pretty")]
    public string? FormulaPretty { get; init; }

    [JsonPropertyName("ordering")]
    public string? Ordering { get; init; }

    [JsonPropertyName("is_magnetic")]
    public bool IsMagnetic { get; init; }

    [JsonPropertyName("exchange_symmetry")]
    public string? ExchangeSymmetry { get; init; }

    /// <summary>
    /// Total magnetization of the material in Bohr magnetons (mu_B).
    /// </summary>
    [JsonPropertyName("total_magnetization")]
    public double? TotalMagnetization { get; init; }

    /// <summary>
    /// Normalized total magnetization per unit formula.
    /// </summary>
    [JsonPropertyName("total_magnetization_normalized_formula_units")]
    public double? TotalMagnetizationNormalizedFormulaUnits { get; init; }

    /// <summary>
    /// Normalized total magnetization per unit volume (mu_B / Angstrom^3).
    /// </summary>
    [JsonPropertyName("total_magnetization_normalized_vol")]
    public double? TotalMagnetizationNormalizedVol { get; init; }

    [JsonPropertyName("num_magnetic_sites")]
    public int? NumMagneticSites { get; init; }
}

public record MagnetismSearchFilter
{
    public string? MaterialId { get; init; }
    public string? Formula { get; init; }
    public string? Chemsys { get; init; }
    public string? Ordering { get; init; }
    public bool? IsMagnetic { get; init; }
    public double? TotalMagnetizationMin { get; init; }
    public double? TotalMagnetizationMax { get; init; }
    public IReadOnlyList<string>? Fields { get; init; }
    public int? Limit { get; init; }
    public int? Skip { get; init; }

    public IDictionary<string, string?> ToQueryParameters()
    {
        var dict = new Dictionary<string, string?>();

        if (!string.IsNullOrWhiteSpace(MaterialId)) dict["material_ids"] = MaterialId;
        if (!string.IsNullOrWhiteSpace(Formula)) dict["formula"] = Formula;
        if (!string.IsNullOrWhiteSpace(Chemsys)) dict["chemsys"] = Chemsys;
        if (!string.IsNullOrWhiteSpace(Ordering)) dict["ordering"] = Ordering;

        if (IsMagnetic.HasValue) dict["is_magnetic"] = IsMagnetic.Value.ToString().ToLowerInvariant();
        if (TotalMagnetizationMin.HasValue) dict["total_magnetization_min"] = TotalMagnetizationMin.Value.ToString();
        if (TotalMagnetizationMax.HasValue) dict["total_magnetization_max"] = TotalMagnetizationMax.Value.ToString();

        if (Fields?.Count > 0) dict["_fields"] = string.Join(",", Fields);
        if (Limit.HasValue) dict["_limit"] = Limit.Value.ToString();
        if (Skip.HasValue) dict["_skip"] = Skip.Value.ToString();

        return dict;
    }
}
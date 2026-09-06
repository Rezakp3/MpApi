using System.Text.Json.Serialization;

namespace MpApi.Domains.Mechanical.Piezoelectric.Models;

/// <summary>
/// Represents piezoelectric properties and electromechanical response tensors.
/// </summary>
public record PiezoelectricDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("formula_pretty")]
    public string? FormulaPretty { get; init; }

    /// <summary>
    /// 3x6 piezoelectric stress tensor (e_ij in C/m^2).
    /// </summary>
    [JsonPropertyName("piezoelectric_tensor")]
    public IReadOnlyList<IReadOnlyList<double>>? PiezoelectricTensor { get; init; }

    /// <summary>
    /// Maximum piezoelectric coefficient in C/m^2.
    /// </summary>
    [JsonPropertyName("e_ij_max")]
    public double? EIjMax { get; init; }

    /// <summary>
    /// 3x6 piezoelectric strain tensor (d_ij in pC/N).
    /// </summary>
    [JsonPropertyName("d_ij")]
    public IReadOnlyList<IReadOnlyList<double>>? DIj { get; init; }

    /// <summary>
    /// Maximum piezoelectric strain coefficient in pC/N.
    /// </summary>
    [JsonPropertyName("d_ij_max")]
    public double? DIjMax { get; init; }
}

public record PiezoelectricSearchFilter
{
    public string? MaterialId { get; init; }
    public string? Formula { get; init; }
    public string? Chemsys { get; init; }
    public double? EIjMaxMin { get; init; }
    public double? EIjMaxMax { get; init; }
    public IReadOnlyList<string>? Fields { get; init; }
    public int? Limit { get; init; }
    public int? Skip { get; init; }

    public IDictionary<string, string?> ToQueryParameters()
    {
        var dict = new Dictionary<string, string?>();

        if (!string.IsNullOrWhiteSpace(MaterialId)) dict["material_ids"] = MaterialId;
        if (!string.IsNullOrWhiteSpace(Formula)) dict["formula"] = Formula;
        if (!string.IsNullOrWhiteSpace(Chemsys)) dict["chemsys"] = Chemsys;

        if (EIjMaxMin.HasValue) dict["e_ij_max_min"] = EIjMaxMin.Value.ToString();
        if (EIjMaxMax.HasValue) dict["e_ij_max_max"] = EIjMaxMax.Value.ToString();

        if (Fields?.Count > 0) dict["_fields"] = string.Join(",", Fields);
        if (Limit.HasValue) dict["_limit"] = Limit.Value.ToString();
        if (Skip.HasValue) dict["_skip"] = Skip.Value.ToString();

        return dict;
    }
}
using System.Text.Json.Serialization;

namespace MpApi.Domains.Electronic.ElectronicStructure.Models;

/// <summary>
/// Represents calculated electronic structure properties, including band gaps and energy extrema.
/// </summary>
public record ElectronicStructureDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("formula_pretty")]
    public string? FormulaPretty { get; init; }

    [JsonPropertyName("band_gap")]
    public double? BandGap { get; init; }

    [JsonPropertyName("cbm")]
    public double? Cbm { get; init; }

    [JsonPropertyName("vbm")]
    public double? Vbm { get; init; }

    [JsonPropertyName("efermi")]
    public double? Efermi { get; init; }

    [JsonPropertyName("is_gap_direct")]
    public bool? IsGapDirect { get; init; }

    [JsonPropertyName("is_metal")]
    public bool? IsMetal { get; init; }

    [JsonPropertyName("magnetic_ordering")]
    public string? MagneticOrdering { get; init; }
}

/// <summary>
/// Filter criteria for querying electronic structure properties.
/// </summary>
public record ElectronicStructureSearchFilter
{
    public string? MaterialId { get; init; }
    public string? Formula { get; init; }
    public string? Chemsys { get; init; }
    public bool? IsMetal { get; init; }
    public bool? IsGapDirect { get; init; }
    public double? BandGapMin { get; init; }
    public double? BandGapMax { get; init; }
    public IReadOnlyList<string>? Fields { get; init; }
    public int? Limit { get; init; }
    public int? Skip { get; init; }

    public IDictionary<string, string?> ToQueryParameters()
    {
        var dict = new Dictionary<string, string?>();

        if (!string.IsNullOrWhiteSpace(MaterialId)) dict["material_ids"] = MaterialId;
        if (!string.IsNullOrWhiteSpace(Formula)) dict["formula"] = Formula;
        if (!string.IsNullOrWhiteSpace(Chemsys)) dict["chemsys"] = Chemsys;

        if (IsMetal.HasValue) dict["is_metal"] = IsMetal.Value.ToString().ToLowerInvariant();
        if (IsGapDirect.HasValue) dict["is_gap_direct"] = IsGapDirect.Value.ToString().ToLowerInvariant();
        if (BandGapMin.HasValue) dict["band_gap_min"] = BandGapMin.Value.ToString();
        if (BandGapMax.HasValue) dict["band_gap_max"] = BandGapMax.Value.ToString();

        if (Fields?.Count > 0) dict["_fields"] = string.Join(",", Fields);
        if (Limit.HasValue) dict["_limit"] = Limit.Value.ToString();
        if (Skip.HasValue) dict["_skip"] = Skip.Value.ToString();

        return dict;
    }
}
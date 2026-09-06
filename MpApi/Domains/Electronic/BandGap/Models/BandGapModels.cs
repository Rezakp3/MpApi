using System.Text.Json.Serialization;

namespace MpApi.Domains.Electronic.BandGap.Models;

/// <summary>
/// Represents accurate calculated band gap data across multiple DFT functional levels of theory (e.g. GGA, HSE06, SCAN).
/// </summary>
public record BandGapDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("formula_pretty")]
    public string? FormulaPretty { get; init; }

    /// <summary>
    /// Calculated band gap in eV.
    /// </summary>
    [JsonPropertyName("band_gap")]
    public double? BandGap { get; init; }

    /// <summary>
    /// Indicates whether the electronic band gap transition is direct.
    /// </summary>
    [JsonPropertyName("is_gap_direct")]
    public bool? IsGapDirect { get; init; }

    /// <summary>
    /// Indicates whether the material is metallic (zero band gap).
    /// </summary>
    [JsonPropertyName("is_metal")]
    public bool? IsMetal { get; init; }

    /// <summary>
    /// Exchange-correlation functional approximation used (e.g. "GGA", "HSE06", "SCAN", "PBE").
    /// </summary>
    [JsonPropertyName("functional")]
    public string? Functional { get; init; }

    /// <summary>
    /// Calculation methodology or code used.
    /// </summary>
    [JsonPropertyName("method")]
    public string? Method { get; init; }

    /// <summary>
    /// Conduction Band Minimum (CBM) energy in eV.
    /// </summary>
    [JsonPropertyName("cbm")]
    public double? Cbm { get; init; }

    /// <summary>
    /// Valence Band Maximum (VBM) energy in eV.
    /// </summary>
    [JsonPropertyName("vbm")]
    public double? Vbm { get; init; }
}

/// <summary>
/// Search criteria and filters for querying band gap properties across various functionals.
/// </summary>
public record BandGapSearchFilter
{
    public string? MaterialId { get; init; }
    public string? Formula { get; init; }
    public string? Chemsys { get; init; }
    public string? Functional { get; init; }
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
        if (!string.IsNullOrWhiteSpace(Functional)) dict["functional"] = Functional;

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
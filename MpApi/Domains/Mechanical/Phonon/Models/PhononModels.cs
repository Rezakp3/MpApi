using System.Text.Json.Serialization;

namespace MpApi.Domains.Mechanical.Phonon.Models;

/// <summary>
/// Represents vibrational phonon properties and dynamical lattice stability.
/// </summary>
public record PhononDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("formula_pretty")]
    public string? FormulaPretty { get; init; }

    /// <summary>
    /// Indicates whether imaginary phonon frequencies exist (indicating dynamic lattice instability).
    /// </summary>
    [JsonPropertyName("has_imaginary_modes")]
    public bool? HasImaginaryModes { get; init; }

    [JsonPropertyName("ph_bandstructure")]
    public object? PhononBandStructure { get; init; }

    [JsonPropertyName("ph_dos")]
    public object? PhononDos { get; init; }
}

public record PhononSearchFilter
{
    public string? MaterialId { get; init; }
    public string? Formula { get; init; }
    public string? Chemsys { get; init; }
    public bool? HasImaginaryModes { get; init; }
    public IReadOnlyList<string>? Fields { get; init; }
    public int? Limit { get; init; }
    public int? Skip { get; init; }

    public IDictionary<string, string?> ToQueryParameters()
    {
        var dict = new Dictionary<string, string?>();

        if (!string.IsNullOrWhiteSpace(MaterialId)) dict["material_ids"] = MaterialId;
        if (!string.IsNullOrWhiteSpace(Formula)) dict["formula"] = Formula;
        if (!string.IsNullOrWhiteSpace(Chemsys)) dict["chemsys"] = Chemsys;
        if (HasImaginaryModes.HasValue) dict["has_imaginary_modes"] = HasImaginaryModes.Value.ToString().ToLowerInvariant();

        if (Fields?.Count > 0) dict["_fields"] = string.Join(",", Fields);
        if (Limit.HasValue) dict["_limit"] = Limit.Value.ToString();
        if (Skip.HasValue) dict["_skip"] = Skip.Value.ToString();

        return dict;
    }
}
using MpApi.Materials.Models;

namespace MpApi.Materials;

/// <summary>
/// Strongly-typed query filter for searching materials summaries.
/// </summary>
public sealed record SummarySearchCriteria
{
    public IReadOnlyList<string>? MaterialIds { get; init; }
    public IReadOnlyList<string>? Elements { get; init; }
    public string? Formula { get; init; }
    public string? Chemsys { get; init; }

    // Ranges
    public (double? Min, double? Max)? BandGap { get; init; }
    public (double? Min, double? Max)? FormationEnergyPerAtom { get; init; }
    public (double? Min, double? Max)? EnergyAboveHull { get; init; }
    public (double? Min, double? Max)? Density { get; init; }
    public (double? Min, double? Max)? Volume { get; init; }

    // Flags
    public bool? IsStable { get; init; }
    public bool? IsMetal { get; init; }
    public bool? IsMagnetic { get; init; }
    public CrystalSystem? CrystalSystem { get; init; }

    // Projections & Pagination
    public IReadOnlyList<string>? Fields { get; init; }
    public int? Limit { get; init; }
    public int? Skip { get; init; }

    /// <summary>
    /// Converts criteria to API query string parameters.
    /// </summary>
    internal Dictionary<string, object?> ToQueryParameters()
    {
        var parameters = new Dictionary<string, object?>();

        if (MaterialIds is { Count: > 0 })
            parameters["material_ids"] = string.Join(",", MaterialIds);

        if (Elements is { Count: > 0 })
            parameters["elements"] = string.Join(",", Elements);

        if (!string.IsNullOrWhiteSpace(Formula))
            parameters["formula"] = Formula;

        if (!string.IsNullOrWhiteSpace(Chemsys))
            parameters["chemsys"] = Chemsys;

        if (IsStable.HasValue) parameters["is_stable"] = IsStable.Value;
        if (IsMetal.HasValue) parameters["is_metal"] = IsMetal.Value;
        if (IsMagnetic.HasValue) parameters["is_magnetic"] = IsMagnetic.Value;
        if (CrystalSystem.HasValue) parameters["crystal_system"] = CrystalSystem.Value.ToString();

        // Numeric ranges
        if (BandGap?.Min is not null) parameters["band_gap_min"] = BandGap.Value.Min;
        if (BandGap?.Max is not null) parameters["band_gap_max"] = BandGap.Value.Max;

        if (FormationEnergyPerAtom?.Min is not null) parameters["formation_energy_per_atom_min"] = FormationEnergyPerAtom.Value.Min;
        if (FormationEnergyPerAtom?.Max is not null) parameters["formation_energy_per_atom_max"] = FormationEnergyPerAtom.Value.Max;

        if (EnergyAboveHull?.Min is not null) parameters["energy_above_hull_min"] = EnergyAboveHull.Value.Min;
        if (EnergyAboveHull?.Max is not null) parameters["energy_above_hull_max"] = EnergyAboveHull.Value.Max;

        if (Density?.Min is not null) parameters["density_min"] = Density.Value.Min;
        if (Density?.Max is not null) parameters["density_max"] = Density.Value.Max;

        if (Volume?.Min is not null) parameters["volume_min"] = Volume.Value.Min;
        if (Volume?.Max is not null) parameters["volume_max"] = Volume.Value.Max;

        if (Fields is { Count: > 0 })
            parameters["_fields"] = string.Join(",", Fields);

        if (Limit.HasValue) parameters["_limit"] = Limit.Value;
        if (Skip.HasValue) parameters["_skip"] = Skip.Value;

        return parameters;
    }
}
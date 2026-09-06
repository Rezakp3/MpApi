namespace MpApi.Domains.Materials.Summary.Models;

/// <summary>
/// Strongly-typed search and filtering parameters for the Materials Summary endpoint.
/// </summary>
public record SummarySearchFilter
{
    public string? MaterialId { get; init; }
    public string? Formula { get; init; }
    public string? Chemsys { get; init; }
    public IReadOnlyList<string>? Elements { get; init; }
    public string? CrystalSystem { get; init; }
    public int? SpacegroupNumber { get; init; }
    public string? SpacegroupSymbol { get; init; }

    public bool? IsStable { get; init; }
    public bool? IsMetal { get; init; }
    public bool? IsMagnetic { get; init; }
    public bool? IsGapDirect { get; init; }

    public double? BandGapMin { get; init; }
    public double? BandGapMax { get; init; }

    public double? EnergyAboveHullMin { get; init; }
    public double? EnergyAboveHullMax { get; init; }

    public double? FormationEnergyPerAtomMin { get; init; }
    public double? FormationEnergyPerAtomMax { get; init; }

    public double? DensityMin { get; init; }
    public double? DensityMax { get; init; }

    public double? VolumeMin { get; init; }
    public double? VolumeMax { get; init; }

    public int? NsitesMin { get; init; }
    public int? NsitesMax { get; init; }

    public int? NelementsMin { get; init; }
    public int? NelementsMax { get; init; }

    /// <summary>
    /// Specific fields to project in response to reduce payload size.
    /// </summary>
    public IReadOnlyList<string>? Fields { get; init; }

    public int? Limit { get; init; }
    public int? Skip { get; init; }
    public string? SortFields { get; init; }

    /// <summary>
    /// Converts filter properties into query parameter dictionary.
    /// </summary>
    public IDictionary<string, string?> ToQueryParameters()
    {
        var dict = new Dictionary<string, string?>();

        if (!string.IsNullOrWhiteSpace(MaterialId)) dict["material_ids"] = MaterialId;
        if (!string.IsNullOrWhiteSpace(Formula)) dict["formula"] = Formula;
        if (!string.IsNullOrWhiteSpace(Chemsys)) dict["chemsys"] = Chemsys;
        if (Elements?.Count > 0) dict["elements"] = string.Join(",", Elements);
        if (!string.IsNullOrWhiteSpace(CrystalSystem)) dict["crystal_system"] = CrystalSystem;
        if (SpacegroupNumber.HasValue) dict["spacegroup_number"] = SpacegroupNumber.Value.ToString();
        if (!string.IsNullOrWhiteSpace(SpacegroupSymbol)) dict["spacegroup_symbol"] = SpacegroupSymbol;

        if (IsStable.HasValue) dict["is_stable"] = IsStable.Value.ToString().ToLowerInvariant();
        if (IsMetal.HasValue) dict["is_metal"] = IsMetal.Value.ToString().ToLowerInvariant();
        if (IsMagnetic.HasValue) dict["is_magnetic"] = IsMagnetic.Value.ToString().ToLowerInvariant();
        if (IsGapDirect.HasValue) dict["is_gap_direct"] = IsGapDirect.Value.ToString().ToLowerInvariant();

        if (BandGapMin.HasValue) dict["band_gap_min"] = BandGapMin.Value.ToString();
        if (BandGapMax.HasValue) dict["band_gap_max"] = BandGapMax.Value.ToString();

        if (EnergyAboveHullMin.HasValue) dict["energy_above_hull_min"] = EnergyAboveHullMin.Value.ToString();
        if (EnergyAboveHullMax.HasValue) dict["energy_above_hull_max"] = EnergyAboveHullMax.Value.ToString();

        if (FormationEnergyPerAtomMin.HasValue) dict["formation_energy_per_atom_min"] = FormationEnergyPerAtomMin.Value.ToString();
        if (FormationEnergyPerAtomMax.HasValue) dict["formation_energy_per_atom_max"] = FormationEnergyPerAtomMax.Value.ToString();

        if (DensityMin.HasValue) dict["density_min"] = DensityMin.Value.ToString();
        if (DensityMax.HasValue) dict["density_max"] = DensityMax.Value.ToString();

        if (VolumeMin.HasValue) dict["volume_min"] = VolumeMin.Value.ToString();
        if (VolumeMax.HasValue) dict["volume_max"] = VolumeMax.Value.ToString();

        if (NsitesMin.HasValue) dict["nsites_min"] = NsitesMin.Value.ToString();
        if (NsitesMax.HasValue) dict["nsites_max"] = NsitesMax.Value.ToString();

        if (NelementsMin.HasValue) dict["nelements_min"] = NelementsMin.Value.ToString();
        if (NelementsMax.HasValue) dict["nelements_max"] = NelementsMax.Value.ToString();

        if (Fields?.Count > 0) dict["_fields"] = string.Join(",", Fields);
        if (Limit.HasValue) dict["_limit"] = Limit.Value.ToString();
        if (Skip.HasValue) dict["_skip"] = Skip.Value.ToString();
        if (!string.IsNullOrWhiteSpace(SortFields)) dict["_sort_fields"] = SortFields;

        return dict;
    }
}
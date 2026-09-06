namespace MpApi.Domains.Thermodynamics.Thermo.Models;

/// <summary>
/// Filter criteria for querying thermodynamic datasets and phase diagram entries.
/// </summary>
public record ThermoSearchFilter
{
    public string? MaterialId { get; init; }
    public string? Formula { get; init; }
    public string? Chemsys { get; init; }
    public IReadOnlyList<string>? Elements { get; init; }
    public string? ThermoType { get; init; }

    public bool? IsStable { get; init; }
    public double? EnergyAboveHullMin { get; init; }
    public double? EnergyAboveHullMax { get; init; }
    public double? FormationEnergyPerAtomMin { get; init; }
    public double? FormationEnergyPerAtomMax { get; init; }

    public IReadOnlyList<string>? Fields { get; init; }
    public int? Limit { get; init; }
    public int? Skip { get; init; }

    public IDictionary<string, string?> ToQueryParameters()
    {
        var dict = new Dictionary<string, string?>();

        if (!string.IsNullOrWhiteSpace(MaterialId)) dict["material_ids"] = MaterialId;
        if (!string.IsNullOrWhiteSpace(Formula)) dict["formula"] = Formula;
        if (!string.IsNullOrWhiteSpace(Chemsys)) dict["chemsys"] = Chemsys;
        if (Elements?.Count > 0) dict["elements"] = string.Join(",", Elements);
        if (!string.IsNullOrWhiteSpace(ThermoType)) dict["thermo_type"] = ThermoType;

        if (IsStable.HasValue) dict["is_stable"] = IsStable.Value.ToString().ToLowerInvariant();
        if (EnergyAboveHullMin.HasValue) dict["energy_above_hull_min"] = EnergyAboveHullMin.Value.ToString();
        if (EnergyAboveHullMax.HasValue) dict["energy_above_hull_max"] = EnergyAboveHullMax.Value.ToString();
        if (FormationEnergyPerAtomMin.HasValue) dict["formation_energy_per_atom_min"] = FormationEnergyPerAtomMin.Value.ToString();
        if (FormationEnergyPerAtomMax.HasValue) dict["formation_energy_per_atom_max"] = FormationEnergyPerAtomMax.Value.ToString();

        if (Fields?.Count > 0) dict["_fields"] = string.Join(",", Fields);
        if (Limit.HasValue) dict["_limit"] = Limit.Value.ToString();
        if (Skip.HasValue) dict["_skip"] = Skip.Value.ToString();

        return dict;
    }
}
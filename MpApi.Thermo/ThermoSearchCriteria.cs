namespace MpApi.Thermo;

/// <summary>
/// Search criteria for thermodynamic data queries.
/// </summary>
public sealed record ThermoSearchCriteria
{
    public IReadOnlyList<string>? MaterialIds { get; init; }
    public string? Formula { get; init; }
    public string? Chemsys { get; init; }

    public (double? Min, double? Max)? EnergyAboveHull { get; init; }
    public (double? Min, double? Max)? FormationEnergyPerAtom { get; init; }
    public bool? IsStable { get; init; }

    public IReadOnlyList<string>? Fields { get; init; }
    public int? Limit { get; init; }
    public int? Skip { get; init; }

    internal Dictionary<string, object?> ToQueryParameters()
    {
        var dict = new Dictionary<string, object?>();

        if (MaterialIds is { Count: > 0 }) dict["material_ids"] = string.Join(",", MaterialIds);
        if (!string.IsNullOrWhiteSpace(Formula)) dict["formula"] = Formula;
        if (!string.IsNullOrWhiteSpace(Chemsys)) dict["chemsys"] = Chemsys;
        if (IsStable.HasValue) dict["is_stable"] = IsStable.Value;

        if (EnergyAboveHull?.Min is not null) dict["energy_above_hull_min"] = EnergyAboveHull.Value.Min;
        if (EnergyAboveHull?.Max is not null) dict["energy_above_hull_max"] = EnergyAboveHull.Value.Max;

        if (FormationEnergyPerAtom?.Min is not null) dict["formation_energy_per_atom_min"] = FormationEnergyPerAtom.Value.Min;
        if (FormationEnergyPerAtom?.Max is not null) dict["formation_energy_per_atom_max"] = FormationEnergyPerAtom.Value.Max;

        if (Fields is { Count: > 0 }) dict["_fields"] = string.Join(",", Fields);
        if (Limit.HasValue) dict["_limit"] = Limit.Value;
        if (Skip.HasValue) dict["_skip"] = Skip.Value;

        return dict;
    }
}
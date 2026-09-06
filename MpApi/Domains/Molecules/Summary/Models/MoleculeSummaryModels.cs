using System.Text.Json.Serialization;

namespace MpApi.Domains.Molecules.Summary.Models;

/// <summary>
/// Represents comprehensive calculated properties for a non-periodic molecular system.
/// </summary>
public record MoleculeSummaryDoc
{
    [JsonPropertyName("molecule_id")]
    public string MoleculeId { get; init; } = string.Empty;

    [JsonPropertyName("formula_pretty")]
    public string? FormulaPretty { get; init; }

    [JsonPropertyName("smiles")]
    public string? Smiles { get; init; }

    [JsonPropertyName("charge")]
    public int Charge { get; init; }

    [JsonPropertyName("spin_multiplicity")]
    public int SpinMultiplicity { get; init; }

    [JsonPropertyName("natoms")]
    public int? Natoms { get; init; }

    [JsonPropertyName("nelements")]
    public int? Nelements { get; init; }

    [JsonPropertyName("elements")]
    public IReadOnlyList<string>? Elements { get; init; }

    [JsonPropertyName("point_group")]
    public string? PointGroup { get; init; }

    /// <summary>
    /// Highest Occupied Molecular Orbital energy in eV.
    /// </summary>
    [JsonPropertyName("homo")]
    public double? Homo { get; init; }

    /// <summary>
    /// Lowest Unoccupied Molecular Orbital energy in eV.
    /// </summary>
    [JsonPropertyName("lumo")]
    public double? Lumo { get; init; }

    /// <summary>
    /// HOMO-LUMO energy gap in eV.
    /// </summary>
    [JsonPropertyName("gap")]
    public double? Gap { get; init; }

    /// <summary>
    /// Ionization energy in eV.
    /// </summary>
    [JsonPropertyName("ionization_energy")]
    public double? IonizationEnergy { get; init; }

    /// <summary>
    /// Electron affinity in eV.
    /// </summary>
    [JsonPropertyName("electron_affinity")]
    public double? ElectronAffinity { get; init; }
}

public record MoleculeSummarySearchFilter
{
    public string? MoleculeId { get; init; }
    public string? Formula { get; init; }
    public string? Smiles { get; init; }
    public int? Charge { get; init; }
    public int? SpinMultiplicity { get; init; }
    public double? HomoMin { get; init; }
    public double? HomoMax { get; init; }
    public double? LumoMin { get; init; }
    public double? LumoMax { get; init; }
    public double? GapMin { get; init; }
    public double? GapMax { get; init; }
    public IReadOnlyList<string>? Fields { get; init; }
    public int? Limit { get; init; }
    public int? Skip { get; init; }

    public IDictionary<string, string?> ToQueryParameters()
    {
        var dict = new Dictionary<string, string?>();

        if (!string.IsNullOrWhiteSpace(MoleculeId)) dict["molecule_ids"] = MoleculeId;
        if (!string.IsNullOrWhiteSpace(Formula)) dict["formula"] = Formula;
        if (!string.IsNullOrWhiteSpace(Smiles)) dict["smiles"] = Smiles;

        if (Charge.HasValue) dict["charge"] = Charge.Value.ToString();
        if (SpinMultiplicity.HasValue) dict["spin_multiplicity"] = SpinMultiplicity.Value.ToString();

        if (HomoMin.HasValue) dict["homo_min"] = HomoMin.Value.ToString();
        if (HomoMax.HasValue) dict["homo_max"] = HomoMax.Value.ToString();
        if (LumoMin.HasValue) dict["lumo_min"] = LumoMin.Value.ToString();
        if (LumoMax.HasValue) dict["lumo_max"] = LumoMax.Value.ToString();
        if (GapMin.HasValue) dict["gap_min"] = GapMin.Value.ToString();
        if (GapMax.HasValue) dict["gap_max"] = GapMax.Value.ToString();

        if (Fields?.Count > 0) dict["_fields"] = string.Join(",", Fields);
        if (Limit.HasValue) dict["_limit"] = Limit.Value.ToString();
        if (Skip.HasValue) dict["_skip"] = Skip.Value.ToString();

        return dict;
    }
}
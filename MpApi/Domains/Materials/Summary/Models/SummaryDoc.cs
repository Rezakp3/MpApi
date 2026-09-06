using System.Text.Json.Serialization;

namespace MpApi.Domains.Materials.Summary.Models;

/// <summary>
/// Represents comprehensive summary data of a material from Materials Project.
/// </summary>
public record SummaryDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("formula_pretty")]
    public string FormulaPretty { get; init; } = string.Empty;

    [JsonPropertyName("formula_anonymous")]
    public string? FormulaAnonymous { get; init; }

    [JsonPropertyName("chemsys")]
    public string? Chemsys { get; init; }

    [JsonPropertyName("volume")]
    public double? Volume { get; init; }

    [JsonPropertyName("density")]
    public double? Density { get; init; }

    [JsonPropertyName("density_atomic")]
    public double? DensityAtomic { get; init; }

    [JsonPropertyName("symmetry")]
    public SymmetryData? Symmetry { get; init; }

    [JsonPropertyName("nsites")]
    public int? Nsites { get; init; }

    [JsonPropertyName("nelements")]
    public int? Nelements { get; init; }

    [JsonPropertyName("elements")]
    public IReadOnlyList<string>? Elements { get; init; }

    [JsonPropertyName("is_stable")]
    public bool? IsStable { get; init; }

    [JsonPropertyName("is_metal")]
    public bool? IsMetal { get; init; }

    [JsonPropertyName("is_magnetic")]
    public bool? IsMagnetic { get; init; }

    [JsonPropertyName("is_gap_direct")]
    public bool? IsGapDirect { get; init; }

    [JsonPropertyName("band_gap")]
    public double? BandGap { get; init; }

    [JsonPropertyName("cbm")]
    public double? Cbm { get; init; }

    [JsonPropertyName("vbm")]
    public double? Vbm { get; init; }

    [JsonPropertyName("efermi")]
    public double? Efermi { get; init; }

    [JsonPropertyName("formation_energy_per_atom")]
    public double? FormationEnergyPerAtom { get; init; }

    [JsonPropertyName("energy_above_hull")]
    public double? EnergyAboveHull { get; init; }

    [JsonPropertyName("energy_per_atom")]
    public double? EnergyPerAtom { get; init; }

    [JsonPropertyName("uncorrected_energy_per_atom")]
    public double? UncorrectedEnergyPerAtom { get; init; }

    [JsonPropertyName("total_magnetization")]
    public double? TotalMagnetization { get; init; }

    [JsonPropertyName("theoretical")]
    public bool? Theoretical { get; init; }

    [JsonPropertyName("warnings")]
    public IReadOnlyList<string>? Warnings { get; init; }
}

/// <summary>
/// Represents crystal symmetry and space group information.
/// </summary>
public record SymmetryData
{
    [JsonPropertyName("crystal_system")]
    public string? CrystalSystem { get; init; }

    [JsonPropertyName("symbol")]
    public string? Symbol { get; init; }

    [JsonPropertyName("number")]
    public int? Number { get; init; }

    [JsonPropertyName("point_group")]
    public string? PointGroup { get; init; }
}
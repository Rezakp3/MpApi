using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Represents calculated and summarized properties of a material from the Materials Project.
/// </summary>
public sealed record MaterialSummary
{
    [JsonPropertyName("material_id")]
    public required string MaterialId { get; init; }

    [JsonPropertyName("formula_pretty")]
    public string? FormulaPretty { get; init; }

    [JsonPropertyName("formula_reduced")]
    public string? FormulaReduced { get; init; }

    [JsonPropertyName("chemsys")]
    public string? Chemsys { get; init; }

    [JsonPropertyName("elements")]
    public IReadOnlyList<string> Elements { get; init; } = [];

    [JsonPropertyName("nsites")]
    public int? NumberOfSites { get; init; }

    [JsonPropertyName("volume")]
    public double? Volume { get; init; }

    [JsonPropertyName("density")]
    public double? Density { get; init; }

    [JsonPropertyName("density_atomic")]
    public double? DensityAtomic { get; init; }

    // --- Electronic Properties ---
    [JsonPropertyName("band_gap")]
    public double? BandGap { get; init; }

    [JsonPropertyName("is_gap_direct")]
    public bool? IsGapDirect { get; init; }

    [JsonPropertyName("is_metal")]
    public bool? IsMetal { get; init; }

    // --- Thermodynamic & Stability Properties ---
    [JsonPropertyName("formation_energy_per_atom")]
    public double? FormationEnergyPerAtom { get; init; }

    [JsonPropertyName("energy_above_hull")]
    public double? EnergyAboveHull { get; init; }

    [JsonPropertyName("is_stable")]
    public bool? IsStable { get; init; }

    [JsonPropertyName("equilibrium_reaction_energy_per_atom")]
    public double? EquilibriumReactionEnergyPerAtom { get; init; }

    // --- Magnetic Properties ---
    [JsonPropertyName("is_magnetic")]
    public bool? IsMagnetic { get; init; }

    [JsonPropertyName("ordering")]
    public string? MagneticOrdering { get; init; }

    [JsonPropertyName("total_magnetization")]
    public double? TotalMagnetization { get; init; }

    // --- Crystallography ---
    [JsonPropertyName("symmetry")]
    public SymmetryData? Symmetry { get; init; }

    [JsonPropertyName("theoretical")]
    public bool? Theoretical { get; init; }

    [JsonPropertyName("deprecated")]
    public bool? Deprecated { get; init; }
}
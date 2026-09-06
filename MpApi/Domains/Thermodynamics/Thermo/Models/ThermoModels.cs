using System.Text.Json.Serialization;

namespace MpApi.Domains.Thermodynamics.Thermo.Models;

/// <summary>
/// Represents thermodynamic calculation data, stability metrics, and phase boundary parameters.
/// </summary>
public record ThermoDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("formula_pretty")]
    public string? FormulaPretty { get; init; }

    [JsonPropertyName("chemsys")]
    public string? Chemsys { get; init; }

    [JsonPropertyName("thermo_type")]
    public string? ThermoType { get; init; }

    /// <summary>
    /// Indicates whether the material is thermodynamically stable (lies directly on the convex hull).
    /// </summary>
    [JsonPropertyName("is_stable")]
    public bool IsStable { get; init; }

    /// <summary>
    /// Energy distance from the convex hull in eV/atom (0.0 means fully stable).
    /// </summary>
    [JsonPropertyName("energy_above_hull")]
    public double EnergyAboveHull { get; init; }

    /// <summary>
    /// Formation energy per atom in eV/atom.
    /// </summary>
    [JsonPropertyName("formation_energy_per_atom")]
    public double? FormationEnergyPerAtom { get; init; }

    /// <summary>
    /// Total corrected DFT energy per atom in eV/atom.
    /// </summary>
    [JsonPropertyName("energy_per_atom")]
    public double? EnergyPerAtom { get; init; }

    /// <summary>
    /// Raw uncorrected DFT calculated energy per atom in eV/atom.
    /// </summary>
    [JsonPropertyName("uncorrected_energy_per_atom")]
    public double? UncorrectedEnergyPerAtom { get; init; }

    /// <summary>
    /// Energy per atom released during equilibrium decomposition reaction.
    /// </summary>
    [JsonPropertyName("equilibrium_reaction_energy_per_atom")]
    public double? EquilibriumReactionEnergyPerAtom { get; init; }

    /// <summary>
    /// List of phase products this material decomposes into if unstable.
    /// </summary>
    [JsonPropertyName("decomposes_to")]
    public IReadOnlyList<DecompositionProduct>? DecomposesTo { get; init; }

    [JsonPropertyName("energy_uncertainties_per_atom")]
    public double? EnergyUncertaintiesPerAtom { get; init; }
}

/// <summary>
/// Represents a decomposition product phase with its stoichiometric ratio.
/// </summary>
public record DecompositionProduct
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("formula")]
    public string? Formula { get; init; }

    [JsonPropertyName("amount")]
    public double Amount { get; init; }
}
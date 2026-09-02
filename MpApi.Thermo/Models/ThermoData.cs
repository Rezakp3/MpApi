using System.Text.Json.Serialization;

namespace MpApi.Thermo.Models;

/// <summary>
/// Thermodynamic calculation results and phase stability properties for a material.
/// </summary>
public sealed record ThermoData
{
    [JsonPropertyName("material_id")]
    public required string MaterialId { get; init; }

    [JsonPropertyName("formula_pretty")]
    public string? FormulaPretty { get; init; }

    [JsonPropertyName("uncorrected_energy_per_atom")]
    public double? UncorrectedEnergyPerAtom { get; init; }

    [JsonPropertyName("energy_per_atom")]
    public double? EnergyPerAtom { get; init; }

    [JsonPropertyName("formation_energy_per_atom")]
    public double? FormationEnergyPerAtom { get; init; }

    [JsonPropertyName("energy_above_hull")]
    public double? EnergyAboveHull { get; init; }

    [JsonPropertyName("is_stable")]
    public bool? IsStable { get; init; }

    [JsonPropertyName("equilibrium_reaction_energy_per_atom")]
    public double? EquilibriumReactionEnergyPerAtom { get; init; }

    [JsonPropertyName("thermo_type")]
    public string? ThermoType { get; init; }
}
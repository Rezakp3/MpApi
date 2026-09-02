using System.Text.Json.Serialization;

namespace MpApi.Molecules.Models;

/// <summary>
/// Calculated quantum-mechanical properties of a molecule.
/// </summary>
public sealed record MoleculeSummary
{
    [JsonPropertyName("task_id")]
    public required string MoleculeId { get; init; }

    [JsonPropertyName("formula_alphabetical")]
    public string? Formula { get; init; }

    [JsonPropertyName("smiles")]
    public string? Smiles { get; init; }

    [JsonPropertyName("charge")]
    public int Charge { get; init; }

    [JsonPropertyName("spin_multiplicity")]
    public int SpinMultiplicity { get; init; }

    [JsonPropertyName("nelements")]
    public int? NumberOfElements { get; init; }

    [JsonPropertyName("elements")]
    public IReadOnlyList<string> Elements { get; init; } = [];

    // --- Electronic Orbitals ---
    [JsonPropertyName("homo")]
    public double? HomoEnergy { get; init; }

    [JsonPropertyName("lumo")]
    public double? LumoEnergy { get; init; }

    [JsonPropertyName("gap")]
    public double? HomoLumoGap { get; init; }

    // --- Physical & Dipole Properties ---
    [JsonPropertyName("dipole_moment")]
    public double? DipoleMoment { get; init; }

    [JsonPropertyName("electron_affinity")]
    public double? ElectronAffinity { get; init; }

    [JsonPropertyName("ionization_energy")]
    public double? IonizationEnergy { get; init; }
}
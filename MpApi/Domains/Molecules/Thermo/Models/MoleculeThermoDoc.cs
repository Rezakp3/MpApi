using System.Text.Json.Serialization;

namespace MpApi.Domains.Molecules.Thermo.Models;

public record MoleculeThermoDoc
{
    [JsonPropertyName("molecule_id")]
    public string MoleculeId { get; init; } = string.Empty;

    [JsonPropertyName("formula_pretty")]
    public string? FormulaPretty { get; init; }

    [JsonPropertyName("total_energy")]
    public double? TotalEnergy { get; init; }

    [JsonPropertyName("enthalpy")]
    public double? Enthalpy { get; init; }

    [JsonPropertyName("entropy")]
    public double? Entropy { get; init; }

    [JsonPropertyName("gibbs_free_energy")]
    public double? GibbsFreeEnergy { get; init; }

    [JsonPropertyName("zero_point_energy")]
    public double? ZeroPointEnergy { get; init; }
}

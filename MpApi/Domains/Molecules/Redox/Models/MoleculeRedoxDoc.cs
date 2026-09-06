using System.Text.Json.Serialization;

namespace MpApi.Domains.Molecules.Redox.Models;

public record MoleculeRedoxDoc
{
    [JsonPropertyName("pair_id")]
    public string PairId { get; init; } = string.Empty;

    [JsonPropertyName("reduction_potential")]
    public double? ReductionPotential { get; init; }

    [JsonPropertyName("oxidation_potential")]
    public double? OxidationPotential { get; init; }

    [JsonPropertyName("electron_transfer")]
    public int? ElectronTransfer { get; init; }
}

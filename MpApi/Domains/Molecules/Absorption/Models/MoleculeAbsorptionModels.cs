using System.Text.Json.Serialization;
using MpApi.Common.Models;

namespace MpApi.Domains.Molecules.Absorption.Models;

public record MoleculeAbsorptionDoc
{
    [JsonPropertyName("molecule_id")]
    public string MoleculeId { get; init; } = string.Empty;

    [JsonPropertyName("energy")]
    public IReadOnlyList<double>? Energy { get; init; }

    [JsonPropertyName("oscillator_strength")]
    public IReadOnlyList<double>? OscillatorStrength { get; init; }
}

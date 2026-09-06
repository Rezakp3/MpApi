using System.Text.Json.Serialization;

namespace MpApi.Domains.Applications.Alloys.Models;

public record AlloyDoc
{
    [JsonPropertyName("pair_id")]
    public string PairId { get; init; } = string.Empty;

    [JsonPropertyName("formula")]
    public string? Formula { get; init; }

    [JsonPropertyName("mixing_energy")]
    public double? MixingEnergy { get; init; }
}

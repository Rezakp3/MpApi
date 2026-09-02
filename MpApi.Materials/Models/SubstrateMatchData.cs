using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Represents epitaxial substrate match data for thin film growth.
/// </summary>
public sealed record SubstrateMatchData
{
    [JsonPropertyName("film_id")]
    public required string FilmId { get; init; }

    [JsonPropertyName("sub_id")]
    public required string SubstrateId { get; init; }

    [JsonPropertyName("film_orientation")]
    public IReadOnlyList<int> FilmOrientation { get; init; } = []; // Miller indices [h, k, l]

    [JsonPropertyName("sub_orientation")]
    public IReadOnlyList<int> SubstrateOrientation { get; init; } = [];

    [JsonPropertyName("area")]
    public double? Area { get; init; }

    [JsonPropertyName("energy")]
    public double? MatchingEnergy { get; init; }

    [JsonPropertyName("mismatch")]
    public double? LatticeMismatch { get; init; }
}
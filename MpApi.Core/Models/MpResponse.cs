using System.Text.Json.Serialization;

namespace MpApi.Core.Models;

/// <summary>
/// Represents the standard envelope returned by the Materials Project Next-Gen API.
/// </summary>
/// <typeparam name="T">The type of the data items payload.</typeparam>
public sealed record MpResponse<T>
{
    [JsonPropertyName("data")]
    public IReadOnlyList<T> Data { get; init; } = [];

    [JsonPropertyName("meta")]
    public MpResponseMetadata? Meta { get; init; }
}

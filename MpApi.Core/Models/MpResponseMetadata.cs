using System.Text.Json.Serialization;

namespace MpApi.Core.Models;

/// <summary>
/// Metadata returned with paginated API responses.
/// </summary>
public sealed record MpResponseMetadata
{
    [JsonPropertyName("total_doc")]
    public int? TotalDocCount { get; init; }

    [JsonPropertyName("total_pages")]
    public int? TotalPages { get; init; }

    [JsonPropertyName("page")]
    public int? Page { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("version")]
    public string? Version { get; init; }
}
using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Represents calculated X-ray absorption spectroscopy (XAS) data.
/// </summary>
public sealed record XasData
{
    [JsonPropertyName("material_id")]
    public required string MaterialId { get; init; }

    [JsonPropertyName("spectrum_type")]
    public string? SpectrumType { get; init; } // e.g. "XANES", "EXAFS"

    [JsonPropertyName("edge")]
    public string? Edge { get; init; } // e.g. "K", "L2,3"

    [JsonPropertyName("absorbing_element")]
    public string? AbsorbingElement { get; init; }

    [JsonPropertyName("spectrum")]
    public IReadOnlyList<IReadOnlyList<double>>? Spectrum { get; init; } = []; // Array of [Energy, Intensity] pairs
}
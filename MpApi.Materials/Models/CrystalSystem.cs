using System.Text.Json.Serialization;

namespace MpApi.Materials.Models;

/// <summary>
/// Defines the seven crystal lattice systems in crystallography.
/// </summary>
public enum CrystalSystem
{
    [JsonPropertyName("Triclinic")]
    Triclinic,

    [JsonPropertyName("Monoclinic")]
    Monoclinic,

    [JsonPropertyName("Orthorhombic")]
    Orthorhombic,

    [JsonPropertyName("Tetragonal")]
    Tetragonal,

    [JsonPropertyName("Trigonal")]
    Trigonal,

    [JsonPropertyName("Hexagonal")]
    Hexagonal,

    [JsonPropertyName("Cubic")]
    Cubic
}
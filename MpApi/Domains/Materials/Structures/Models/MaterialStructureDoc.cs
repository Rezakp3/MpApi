using System.Text.Json.Serialization;

namespace MpApi.Domains.Materials.Structures.Models;

/// <summary>
/// Represents full crystal structure and atomic coordinates for a material.
/// </summary>
public record MaterialStructureDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("formula_pretty")]
    public string? FormulaPretty { get; init; }

    [JsonPropertyName("structure")]
    public CrystalStructure? Structure { get; init; }

    [JsonPropertyName("cif")]
    public string? Cif { get; init; }

    [JsonPropertyName("deprecated")]
    public bool Deprecated { get; init; }

    [JsonPropertyName("deprecation_reasons")]
    public IReadOnlyList<string>? DeprecationReasons { get; init; }
}

/// <summary>
/// Detailed crystal structure containing unit cell lattice parameters and atomic sites.
/// </summary>
public record CrystalStructure
{
    [JsonPropertyName("lattice")]
    public LatticeData? Lattice { get; init; }

    [JsonPropertyName("sites")]
    public IReadOnlyList<SiteData>? Sites { get; init; }

    [JsonPropertyName("charge")]
    public double? Charge { get; init; }
}

/// <summary>
/// Real-space unit cell lattice parameters, vectors, and angles.
/// </summary>
public record LatticeData
{
    /// <summary>
    /// 3x3 transformation matrix defining the unit cell vectors in Cartesian space (in Angstroms).
    /// </summary>
    [JsonPropertyName("matrix")]
    public IReadOnlyList<IReadOnlyList<double>>? Matrix { get; init; }

    [JsonPropertyName("a")]
    public double A { get; init; }

    [JsonPropertyName("b")]
    public double B { get; init; }

    [JsonPropertyName("c")]
    public double C { get; init; }

    [JsonPropertyName("alpha")]
    public double Alpha { get; init; }

    [JsonPropertyName("beta")]
    public double Beta { get; init; }

    [JsonPropertyName("gamma")]
    public double Gamma { get; init; }

    [JsonPropertyName("volume")]
    public double Volume { get; init; }
}

/// <summary>
/// Represents an atomic site with position coordinates and elemental occupancy.
/// </summary>
public record SiteData
{
    [JsonPropertyName("species")]
    public IReadOnlyList<SpeciesOccupancy>? Species { get; init; }

    /// <summary>
    /// Fractional coordinates (a, b, c) within the unit cell.
    /// </summary>
    [JsonPropertyName("abc")]
    public IReadOnlyList<double>? FractionalCoordinates { get; init; }

    /// <summary>
    /// Cartesian coordinates (x, y, z) in Angstroms.
    /// </summary>
    [JsonPropertyName("xyz")]
    public IReadOnlyList<double>? CartesianCoordinates { get; init; }

    [JsonPropertyName("label")]
    public string? Label { get; init; }

    [JsonPropertyName("properties")]
    public IReadOnlyDictionary<string, object>? Properties { get; init; }
}

/// <summary>
/// Chemical species element symbol and its occupancy fraction.
/// </summary>
public record SpeciesOccupancy
{
    [JsonPropertyName("element")]
    public string Element { get; init; } = string.Empty;

    [JsonPropertyName("occu")]
    public double Occupancy { get; init; } = 1.0;
}
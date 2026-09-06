using System.Text.Json.Serialization;

namespace MpApi.Domains.Applications.Substrates.Models;

public record SubstrateDoc
{
    [JsonPropertyName("material_id")]
    public string MaterialId { get; init; } = string.Empty;

    [JsonPropertyName("substrate_id")]
    public string? SubstrateId { get; init; }

    [JsonPropertyName("substrate_formula")]
    public string? SubstrateFormula { get; init; }

    [JsonPropertyName("film_miller")]
    public IReadOnlyList<int>? FilmMiller { get; init; }

    [JsonPropertyName("substrate_miller")]
    public IReadOnlyList<int>? SubstrateMiller { get; init; }

    [JsonPropertyName("area_mismatch")]
    public double? AreaMismatch { get; init; }

    [JsonPropertyName("elastic_energy")]
    public double? ElasticEnergy { get; init; }
}

public record SubstrateSearchFilter
{
    public string? MaterialId { get; init; }
    public string? SubstrateFormula { get; init; }
    public double? MaxAreaMismatch { get; init; }
    public double? MaxElasticEnergy { get; init; }
    public IReadOnlyList<string>? Fields { get; init; }
    public int? Limit { get; init; }
    public int? Skip { get; init; }

    public IDictionary<string, string?> ToQueryParameters()
    {
        var dict = new Dictionary<string, string?>();
        if (!string.IsNullOrWhiteSpace(MaterialId)) dict["material_ids"] = MaterialId;
        if (!string.IsNullOrWhiteSpace(SubstrateFormula)) dict["substrate_formula"] = SubstrateFormula;
        if (MaxAreaMismatch.HasValue) dict["area_mismatch_max"] = MaxAreaMismatch.Value.ToString();
        if (MaxElasticEnergy.HasValue) dict["elastic_energy_max"] = MaxElasticEnergy.Value.ToString();

        if (Fields?.Count > 0) dict["_fields"] = string.Join(",", Fields);
        if (Limit.HasValue) dict["_limit"] = Limit.Value.ToString();
        if (Skip.HasValue) dict["_skip"] = Skip.Value.ToString();
        return dict;
    }
}
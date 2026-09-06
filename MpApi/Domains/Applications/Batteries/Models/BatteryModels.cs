using System.Text.Json.Serialization;

namespace MpApi.Domains.Applications.Batteries.Models;

/// <summary>
/// Represents calculated battery insertion electrode performance and electrochemical statistics.
/// </summary>
public record BatteryDoc
{
    [JsonPropertyName("battery_id")]
    public string BatteryId { get; init; } = string.Empty;

    [JsonPropertyName("battery_formula")]
    public string? BatteryFormula { get; init; }

    [JsonPropertyName("working_ion")]
    public string? WorkingIon { get; init; }

    [JsonPropertyName("average_voltage")]
    public double? AverageVoltage { get; init; }

    [JsonPropertyName("capacity_grav")]
    public double? GravimetricCapacity { get; init; }

    [JsonPropertyName("capacity_vol")]
    public double? VolumetricCapacity { get; init; }

    [JsonPropertyName("energy_grav")]
    public double? GravimetricEnergy { get; init; }

    [JsonPropertyName("energy_vol")]
    public double? VolumetricEnergy { get; init; }

    [JsonPropertyName("frac_volume_change")]
    public double? FractionalVolumeChange { get; init; }

    [JsonPropertyName("formula_charge")]
    public string? FormulaCharge { get; init; }

    [JsonPropertyName("formula_discharge")]
    public string? FormulaDischarge { get; init; }

    [JsonPropertyName("stability_charge")]
    public double? StabilityCharge { get; init; }

    [JsonPropertyName("stability_discharge")]
    public double? StabilityDischarge { get; init; }
}

public record BatterySearchFilter
{
    public string? BatteryId { get; init; }
    public string? WorkingIon { get; init; }
    public string? Formula { get; init; }
    public string? Chemsys { get; init; }
    public double? AverageVoltageMin { get; init; }
    public double? AverageVoltageMax { get; init; }
    public double? CapacityGravMin { get; init; }
    public double? CapacityGravMax { get; init; }
    public double? MaxFracVolumeChange { get; init; }
    public IReadOnlyList<string>? Fields { get; init; }
    public int? Limit { get; init; }
    public int? Skip { get; init; }

    public IDictionary<string, string?> ToQueryParameters()
    {
        var dict = new Dictionary<string, string?>();

        if (!string.IsNullOrWhiteSpace(BatteryId)) dict["battery_id"] = BatteryId;
        if (!string.IsNullOrWhiteSpace(WorkingIon)) dict["working_ion"] = WorkingIon;
        if (!string.IsNullOrWhiteSpace(Formula)) dict["formula"] = Formula;
        if (!string.IsNullOrWhiteSpace(Chemsys)) dict["chemsys"] = Chemsys;

        if (AverageVoltageMin.HasValue) dict["average_voltage_min"] = AverageVoltageMin.Value.ToString();
        if (AverageVoltageMax.HasValue) dict["average_voltage_max"] = AverageVoltageMax.Value.ToString();
        if (CapacityGravMin.HasValue) dict["capacity_grav_min"] = CapacityGravMin.Value.ToString();
        if (CapacityGravMax.HasValue) dict["capacity_grav_max"] = CapacityGravMax.Value.ToString();
        if (MaxFracVolumeChange.HasValue) dict["frac_volume_change_max"] = MaxFracVolumeChange.Value.ToString();

        if (Fields?.Count > 0) dict["_fields"] = string.Join(",", Fields);
        if (Limit.HasValue) dict["_limit"] = Limit.Value.ToString();
        if (Skip.HasValue) dict["_skip"] = Skip.Value.ToString();

        return dict;
    }
}
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MpApi.Core.Serialization;

/// <summary>
/// Centralized System.Text.Json options for Materials Project data serialization.
/// </summary>
public static class MpJsonSerializerOptions
{
    public static readonly JsonSerializerOptions Default = CreateDefaultOptions();

    private static JsonSerializerOptions CreateDefaultOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals
        };

        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));
        return options;
    }
}
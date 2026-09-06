using System.Text.Json;
using System.Text.Json.Serialization;

namespace MpApi.Common.Serialization;

/// <summary>
/// Provides configured System.Text.Json options optimized for Materials Project API contracts.
/// </summary>
public static class MpJsonSerializer
{
    /// <summary>
    /// Default serialization options using SnakeCase naming and tolerant deserialization settings.
    /// </summary>
    public static readonly JsonSerializerOptions Options = CreateDefaultOptions();

    private static JsonSerializerOptions CreateDefaultOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));

        return options;
    }
}
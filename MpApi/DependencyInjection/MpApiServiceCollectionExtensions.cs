using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MpApi.Core.Authentication;
using MpApi.Core.Http;
using MpApi.Core.Options;
using MpApi.Materials;
using MpApi.Molecules;
using MpApi.Synthesis;
using MpApi.Thermo;

namespace MpApi.DependencyInjection;

public static class MpApiServiceCollectionExtensions
{
    /// <summary>
    /// Registers all Materials Project API clients in the DI container with an API key.
    /// </summary>
    public static IServiceCollection AddMpApi(this IServiceCollection services, string apiKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);
        return services.AddMpApi(options => options.ApiKey = apiKey);
    }

    /// <summary>
    /// Registers all Materials Project API clients in the DI container with full configuration.
    /// </summary>
    public static IServiceCollection AddMpApi(
        this IServiceCollection services,
        Action<MpApiOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureOptions);

        var options = new MpApiOptions { ApiKey = string.Empty };
        configureOptions(options);

        if (string.IsNullOrWhiteSpace(options.ApiKey))
            throw new ArgumentException("API Key must be provided in MpApiOptions.", nameof(configureOptions));

        services.TryAddSingleton(options);
        services.TryAddTransient<MpAuthenticationHandler>();

        // Register Core HTTP Client with SocketsHandler Pooling & Auth Handler
        services.AddHttpClient<IMpHttpClient, MpHttpClient>()
            .AddHttpMessageHandler<MpAuthenticationHandler>();

        // Register Domain Clients
        services.TryAddTransient<IMpMaterialsClient, MpMaterialsClient>();
        services.TryAddTransient<IMpThermoClient, MpThermoClient>();
        services.TryAddTransient<IMpMoleculesClient, MpMoleculesClient>();
        services.TryAddTransient<IMpSynthesisClient, MpSynthesisClient>();

        // Register Root Facade Client
        services.TryAddTransient<IMpApiClient, MpApiClient>();

        return services;
    }
}
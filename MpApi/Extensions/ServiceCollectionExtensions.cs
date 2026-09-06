using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MpApi.Common.Http;

namespace MpApi.Extensions;

/// <summary>
/// Extension methods for setting up Materials Project API client in an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds and configures the Materials Project API client services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configureOptions">Delegate to configure <see cref="MpApiOptions"/>.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddMpApiClient(
        this IServiceCollection services,
        Action<MpApiOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureOptions);

        services.Configure(configureOptions);

        services.AddHttpClient<IMpHttpClient, MpHttpClient>((serviceProvider, httpClient) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<MpApiOptions>>().Value;
            options.Validate();
        });

        services.AddScoped<IMpApiClient, MpApiClient>();

        return services;
    }
}
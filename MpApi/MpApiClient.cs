using MpApi.Core.Authentication;
using MpApi.Core.Http;
using MpApi.Core.Options;
using MpApi.Materials;
using MpApi.Molecules;
using MpApi.Synthesis;
using MpApi.Thermo;

namespace MpApi;

/// <summary>
/// Standalone Facade client for all Materials Project services.
/// Ideal for Desktop (WPF/WinForms), Console tools, and scripts.
/// </summary>
public sealed class MpApiClient : IMpApiClient
{
    private readonly HttpClient? _internalHttpClient;
    private bool _disposed;

    public IMpMaterialsClient Materials { get; }
    public IMpThermoClient Thermo { get; }
    public IMpMoleculesClient Molecules { get; }
    public IMpSynthesisClient Synthesis { get; }

    /// <summary>
    /// Quick standalone constructor with just an API key.
    /// </summary>
    public MpApiClient(string apiKey) : this(new MpApiOptions { ApiKey = apiKey })
    {
    }

    /// <summary>
    /// Standalone constructor with detailed custom options.
    /// </summary>
    public MpApiClient(MpApiOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        var authHandler = new MpAuthenticationHandler(options)
        {
            InnerHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(15),
                EnableMultipleHttp2Connections = true
            }
        };

        _internalHttpClient = new HttpClient(authHandler)
        {
            Timeout = options.Timeout
        };

        var httpEngine = new MpHttpClient(_internalHttpClient, options);

        Materials = new MpMaterialsClient(httpEngine);
        Thermo = new MpThermoClient(httpEngine);
        Molecules = new MpMoleculesClient(httpEngine);
        Synthesis = new MpSynthesisClient(httpEngine);
    }

    /// <summary>
    /// Constructor for DI-based dependency injection.
    /// </summary>
    public MpApiClient(
        IMpMaterialsClient materials,
        IMpThermoClient thermo,
        IMpMoleculesClient molecules,
        IMpSynthesisClient synthesis)
    {
        Materials = materials ?? throw new ArgumentNullException(nameof(materials));
        Thermo = thermo ?? throw new ArgumentNullException(nameof(thermo));
        Molecules = molecules ?? throw new ArgumentNullException(nameof(molecules));
        Synthesis = synthesis ?? throw new ArgumentNullException(nameof(synthesis));
    }

    public void Dispose()
    {
        if (_disposed) return;
        _internalHttpClient?.Dispose();
        _disposed = true;
    }
}
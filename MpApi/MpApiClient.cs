using MpApi.Common.Http;
using MpApi.Domains.Applications;
using MpApi.Domains.Electronic;
using MpApi.Domains.Materials;
using MpApi.Domains.Mechanical;
using MpApi.Domains.Molecules;
using MpApi.Domains.Thermodynamics;

namespace MpApi;

/// <summary>
/// Main client implementation for Materials Project API.
/// Supports both direct instantiation and Dependency Injection.
/// </summary>
public sealed class MpApiClient : IMpApiClient, IDisposable
{
    private readonly HttpClient? _internalHttpClient;
    private readonly IMpHttpClient _mpHttpClient;

    public IMaterialsDomain Materials { get; }
    public IThermodynamicsDomain Thermodynamics { get; }
    public IElectronicDomain Electronic { get; }
    public IMechanicalDomain Mechanical { get; }
    public IApplicationsDomain Applications { get; }
    public IMoleculesDomain Molecules { get; }

    /// <summary>
    /// Initializes a new client instance directly using an API key (Recommended for Console/Desktop apps).
    /// </summary>
    /// <param name="apiKey">Materials Project API key.</param>
    public MpApiClient(string apiKey)
        : this(new MpApiOptions { ApiKey = apiKey })
    {
    }

    /// <summary>
    /// Initializes a new client instance using custom options.
    /// </summary>
    /// <param name="options">Client options.</param>
    public MpApiClient(MpApiOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        options.Validate();

        _internalHttpClient = new HttpClient();
        _mpHttpClient = new MpHttpClient(_internalHttpClient, options);

        Materials = new MaterialsDomain(_mpHttpClient);
        Thermodynamics = new ThermodynamicsDomain(_mpHttpClient);
        Electronic = new ElectronicDomain(_mpHttpClient);
        Mechanical = new MechanicalDomain(_mpHttpClient);
        Applications = new ApplicationsDomain(_mpHttpClient);
        Molecules = new MoleculesDomain(_mpHttpClient);
    }

    /// <summary>
    /// Initializes a new client instance using a shared <see cref="IMpHttpClient"/> (Used by Dependency Injection).
    /// </summary>
    /// <param name="mpHttpClient">Configured internal HTTP client.</param>
    public MpApiClient(IMpHttpClient mpHttpClient)
    {
        _mpHttpClient = mpHttpClient ?? throw new ArgumentNullException(nameof(mpHttpClient));

        Materials = new MaterialsDomain(_mpHttpClient);
        Thermodynamics = new ThermodynamicsDomain(_mpHttpClient);
        Electronic = new ElectronicDomain(_mpHttpClient);
        Mechanical = new MechanicalDomain(_mpHttpClient);
        Applications = new ApplicationsDomain(_mpHttpClient);
        Molecules = new MoleculesDomain(_mpHttpClient);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _internalHttpClient?.Dispose();
    }
}
using MpApi.Common.Http;
using MpApi.Domains.Thermodynamics.Thermo;

namespace MpApi.Domains.Thermodynamics;

/// <summary>
/// Facade providing access to thermodynamic services.
/// </summary>
public sealed class ThermodynamicsDomain : IThermodynamicsDomain
{
    public IThermoClient Thermo { get; }

    public ThermodynamicsDomain(IMpHttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        Thermo = new ThermoClient(httpClient);
    }
}
using MpApi.Domains.Thermodynamics.Thermo;

namespace MpApi.Domains.Thermodynamics;

/// <summary>
/// Unified facade interface for thermodynamic stability and phase boundary services.
/// </summary>
public interface IThermodynamicsDomain
{
    /// <summary>
    /// Thermodynamic properties, formation energies, and phase balance data.
    /// </summary>
    IThermoClient Thermo { get; }
}
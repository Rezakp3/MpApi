using MpApi.Materials;
using MpApi.Molecules;
using MpApi.Synthesis;
using MpApi.Thermo;

namespace MpApi;

/// <summary>
/// Root entry point for accessing all Materials Project API resources.
/// </summary>
public interface IMpApiClient : IDisposable
{
    IMpMaterialsClient Materials { get; }
    IMpThermoClient Thermo { get; }
    IMpMoleculesClient Molecules { get; }
    IMpSynthesisClient Synthesis { get; }
}
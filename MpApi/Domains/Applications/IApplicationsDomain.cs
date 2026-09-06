using MpApi.Domains.Applications.Alloys;
using MpApi.Domains.Applications.Batteries;
using MpApi.Domains.Applications.Substrates;
using MpApi.Domains.Applications.Surfaces;
using MpApi.Domains.Applications.Synthesis;

namespace MpApi.Domains.Applications;

/// <summary>
/// Unified facade interface for industry-oriented materials applications.
/// </summary>
public interface IApplicationsDomain
{
    IBatteriesClient Batteries { get; }
    ISubstratesClient Substrates { get; }
    ISurfacesClient Surfaces { get; }
    IAlloysClient Alloys { get; }
    ISynthesisClient Synthesis { get; }
}

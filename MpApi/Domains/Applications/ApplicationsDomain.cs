using MpApi.Common.Http;
using MpApi.Domains.Applications.Alloys;
using MpApi.Domains.Applications.Batteries;
using MpApi.Domains.Applications.Substrates;
using MpApi.Domains.Applications.Surfaces;
using MpApi.Domains.Applications.Synthesis;

namespace MpApi.Domains.Applications;

public sealed class ApplicationsDomain : IApplicationsDomain
{
    public IBatteriesClient Batteries { get; }
    public ISubstratesClient Substrates { get; }
    public ISurfacesClient Surfaces { get; }
    public IAlloysClient Alloys { get; }
    public ISynthesisClient Synthesis { get; }

    public ApplicationsDomain(IMpHttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);

        Batteries = new BatteriesClient(httpClient);
        Substrates = new SubstratesClient(httpClient);
        Surfaces = new SurfacesClient(httpClient);
        Alloys = new AlloysClient(httpClient);
        Synthesis = new SynthesisClient(httpClient);
    }
}
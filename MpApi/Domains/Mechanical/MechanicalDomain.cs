using MpApi.Common.Http;
using MpApi.Domains.Mechanical.Elasticity;
using MpApi.Domains.Mechanical.Phonon;
using MpApi.Domains.Mechanical.Piezoelectric;

namespace MpApi.Domains.Mechanical;

public sealed class MechanicalDomain : IMechanicalDomain
{
    public IElasticityClient Elasticity { get; }
    public IPiezoelectricClient Piezoelectric { get; }
    public IPhononClient Phonon { get; }

    public MechanicalDomain(IMpHttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);

        Elasticity = new ElasticityClient(httpClient);
        Piezoelectric = new PiezoelectricClient(httpClient);
        Phonon = new PhononClient(httpClient);
    }
}

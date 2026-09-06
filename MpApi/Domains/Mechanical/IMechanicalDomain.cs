using MpApi.Domains.Mechanical.Elasticity;
using MpApi.Domains.Mechanical.Phonon;
using MpApi.Domains.Mechanical.Piezoelectric;

namespace MpApi.Domains.Mechanical;

/// <summary>
/// Unified facade interface for mechanical, elastic, piezoelectric, and vibrational properties.
/// </summary>
public interface IMechanicalDomain
{
    IElasticityClient Elasticity { get; }
    IPiezoelectricClient Piezoelectric { get; }
    IPhononClient Phonon { get; }
}

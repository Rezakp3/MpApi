using MpApi.Common.Http;
using MpApi.Domains.Electronic.BandGap;
using MpApi.Domains.Electronic.Dielectric;
using MpApi.Domains.Electronic.ElectronicStructure;
using MpApi.Domains.Electronic.Magnetism;
using MpApi.Domains.Materials.Xas;

namespace MpApi.Domains.Electronic;

/// <summary>
/// Unified facade interface for electronic, optical, dielectric, and magnetic material properties.
/// </summary>
public interface IElectronicDomain
{
    IElectronicStructureClient ElectronicStructure { get; }
    IDielectricClient Dielectric { get; }
    IMagnetismClient Magnetism { get; }
    IBandGapClient BandGap { get; }
    IXasClient Xas { get; }
}


public sealed class ElectronicDomain : IElectronicDomain
{
    public IElectronicStructureClient ElectronicStructure { get; }
    public IBandGapClient BandGap { get; }
    public IDielectricClient Dielectric { get; }
    public IMagnetismClient Magnetism { get; }
    public IXasClient Xas { get; }

    public ElectronicDomain(IMpHttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);

        ElectronicStructure = new ElectronicStructureClient(httpClient);
        BandGap = new BandGapClient(httpClient);
        Dielectric = new DielectricClient(httpClient);
        Magnetism = new MagnetismClient(httpClient);
        Xas = new XasClient(httpClient);
    }
}
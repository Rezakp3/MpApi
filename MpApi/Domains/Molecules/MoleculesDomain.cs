using MpApi.Common.Http;
using MpApi.Domains.Molecules.Absorption;
using MpApi.Domains.Molecules.Redox;
using MpApi.Domains.Molecules.Summary;
using MpApi.Domains.Molecules.Tasks;
using MpApi.Domains.Molecules.Thermo;

namespace MpApi.Domains.Molecules;

public sealed class MoleculesDomain : IMoleculesDomain
{
    public IMoleculeSummaryClient Summary { get; }
    public IMoleculeThermoClient Thermo { get; }
    public IMoleculeRedoxClient Redox { get; }
    public IMoleculeAbsorptionClient Absorption { get; }
    public IMoleculeTasksClient Tasks { get; }

    public MoleculesDomain(IMpHttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);

        Summary = new MoleculeSummaryClient(httpClient);
        Thermo = new MoleculeThermoClient(httpClient);
        Redox = new MoleculeRedoxClient(httpClient);
        Absorption = new MoleculeAbsorptionClient(httpClient);
        Tasks = new MoleculeTasksClient(httpClient);
    }
}

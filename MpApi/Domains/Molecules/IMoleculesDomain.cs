using MpApi.Domains.Molecules.Absorption;
using MpApi.Domains.Molecules.Redox;
using MpApi.Domains.Molecules.Summary;
using MpApi.Domains.Molecules.Tasks;
using MpApi.Domains.Molecules.Thermo;

namespace MpApi.Domains.Molecules;

public interface IMoleculesDomain
{
    IMoleculeSummaryClient Summary { get; }
    IMoleculeThermoClient Thermo { get; }
    IMoleculeRedoxClient Redox { get; }
    IMoleculeAbsorptionClient Absorption { get; }
    IMoleculeTasksClient Tasks { get; }
}

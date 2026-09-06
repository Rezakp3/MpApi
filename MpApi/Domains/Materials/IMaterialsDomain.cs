using MpApi.Domains.Materials.Chemenv;
using MpApi.Domains.Materials.Robocrys;
using MpApi.Domains.Materials.Similarity;
using MpApi.Domains.Materials.Structures;
using MpApi.Domains.Materials.Summary;
using MpApi.Domains.Materials.Tasks;
using MpApi.Domains.Materials.Xas;

namespace MpApi.Domains.Materials;

public interface IMaterialsDomain
{
    ISummaryClient Summary { get; }
    IStructuresClient Structures { get; }
    IChemenvClient Chemenv { get; }
    ISimilarityClient Similarity { get; }
    IRobocrysClient Robocrys { get; }
    ITasksClient Tasks { get; }
    IXasClient Xas { get; }
}

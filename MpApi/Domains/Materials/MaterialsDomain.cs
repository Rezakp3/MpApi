using MpApi.Common.Http;
using MpApi.Domains.Materials.Chemenv;
using MpApi.Domains.Materials.Robocrys;
using MpApi.Domains.Materials.Similarity;
using MpApi.Domains.Materials.Structures;
using MpApi.Domains.Materials.Summary;
using MpApi.Domains.Materials.Tasks;
using MpApi.Domains.Materials.Xas;

namespace MpApi.Domains.Materials;

public sealed class MaterialsDomain : IMaterialsDomain
{
    public ISummaryClient Summary { get; }
    public IStructuresClient Structures { get; }
    public IChemenvClient Chemenv { get; }
    public ISimilarityClient Similarity { get; }
    public IRobocrysClient Robocrys { get; }
    public ITasksClient Tasks { get; }
    public IXasClient Xas { get; }

    public MaterialsDomain(IMpHttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);

        Summary = new SummaryClient(httpClient);
        Structures = new StructuresClient(httpClient);
        Chemenv = new ChemenvClient(httpClient);
        Similarity = new SimilarityClient(httpClient);
        Robocrys = new RobocrysClient(httpClient);
        Tasks = new TasksClient(httpClient);
        Xas = new XasClient(httpClient);
    }
}
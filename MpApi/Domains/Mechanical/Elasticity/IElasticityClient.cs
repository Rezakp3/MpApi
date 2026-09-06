using MpApi.Common.Models;
using MpApi.Domains.Mechanical.Elasticity.Models;

namespace MpApi.Domains.Mechanical.Elasticity;

public interface IElasticityClient
{
    Task<ElasticityDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default);
    Task<MpResponse<IReadOnlyList<ElasticityDoc>>> SearchAsync(ElasticitySearchFilter filter, CancellationToken cancellationToken = default);
}

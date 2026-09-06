using MpApi.Domains.Materials.Similarity.Models;

namespace MpApi.Domains.Materials.Similarity;

public interface ISimilarityClient
{
    Task<SimilarityDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default);
}

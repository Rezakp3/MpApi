using MpApi.Synthesis.Models;

namespace MpApi.Synthesis;

public interface IMpSynthesisClient
{
    Task<IReadOnlyList<SynthesisRecipe>> SearchByTargetAsync(string targetFormula, CancellationToken cancellationToken = default);
    IAsyncEnumerable<SynthesisRecipe> StreamRecipesAsync(Dictionary<string, object?> queryParams, CancellationToken cancellationToken = default);
}

using MpApi.Common.Http;
using MpApi.Domains.Molecules.Absorption.Models;

namespace MpApi.Domains.Molecules.Absorption;

public sealed class MoleculeAbsorptionClient : IMoleculeAbsorptionClient
{
    private const string BaseEndpoint = "molecules/absorption/";
    private readonly IMpHttpClient _httpClient;

    public MoleculeAbsorptionClient(IMpHttpClient httpClient) => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<MoleculeAbsorptionDoc?> GetByIdAsync(string moleculeId, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?> { ["molecule_ids"] = moleculeId };
        var response = await _httpClient.GetAsync<IReadOnlyList<MoleculeAbsorptionDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
        return response.Data?.FirstOrDefault();
    }
}
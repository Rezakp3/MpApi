using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Molecules.Redox.Models;

namespace MpApi.Domains.Molecules.Redox;

public sealed class MoleculeRedoxClient : IMoleculeRedoxClient
{
    private const string BaseEndpoint = "molecules/redox/";
    private readonly IMpHttpClient _httpClient;

    public MoleculeRedoxClient(IMpHttpClient httpClient) => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<MpResponse<IReadOnlyList<MoleculeRedoxDoc>>> SearchAsync(int limit = 50, CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, string?> { ["_limit"] = limit.ToString() };
        return await _httpClient.GetAsync<IReadOnlyList<MoleculeRedoxDoc>>(BaseEndpoint, query, cancellationToken).ConfigureAwait(false);
    }
}
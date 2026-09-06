using MpApi.Common.Http;
using MpApi.Common.Models;
using MpApi.Domains.Materials.Summary.Models;

namespace MpApi.Domains.Materials.Summary;

/// <summary>
/// Implementation of <see cref="ISummaryClient"/> providing access to the Summary endpoint.
/// </summary>
public sealed class SummaryClient : ISummaryClient
{
    private const string BaseEndpoint = "materials/summary/";
    private readonly IMpHttpClient _httpClient;

    public SummaryClient(IMpHttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<SummaryDoc?> GetByIdAsync(
        string materialId,
        IEnumerable<string>? fields = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(materialId))
        {
            throw new ArgumentException("Material ID cannot be null or empty.", nameof(materialId));
        }

        var queryParams = new Dictionary<string, string?>
        {
            ["material_ids"] = materialId
        };

        if (fields != null)
        {
            var fieldList = fields.ToList();
            if (fieldList.Count > 0)
            {
                queryParams["_fields"] = string.Join(",", fieldList);
            }
        }

        var response = await _httpClient.GetAsync<IReadOnlyList<SummaryDoc>>(BaseEndpoint, queryParams, cancellationToken)
            .ConfigureAwait(false);

        return response.Data?.FirstOrDefault();
    }

    public async Task<MpResponse<IReadOnlyList<SummaryDoc>>> SearchAsync(
        SummarySearchFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        var queryParams = filter.ToQueryParameters();

        return await _httpClient.GetAsync<IReadOnlyList<SummaryDoc>>(BaseEndpoint, queryParams, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<SummaryDoc>> SearchByFormulaAsync(
        string formula,
        int limit = 10,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(formula))
        {
            throw new ArgumentException("Formula cannot be null or empty.", nameof(formula));
        }

        var filter = new SummarySearchFilter
        {
            Formula = formula,
            Limit = limit
        };

        var response = await SearchAsync(filter, cancellationToken).ConfigureAwait(false);
        return response.Data ?? Array.Empty<SummaryDoc>();
    }

    public async Task<IReadOnlyList<SummaryDoc>> SearchByChemsysAsync(
        string chemsys,
        int limit = 50,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(chemsys))
        {
            throw new ArgumentException("Chemical system cannot be null or empty.", nameof(chemsys));
        }

        var filter = new SummarySearchFilter
        {
            Chemsys = chemsys,
            Limit = limit
        };

        var response = await SearchAsync(filter, cancellationToken).ConfigureAwait(false);
        return response.Data ?? Array.Empty<SummaryDoc>();
    }
}
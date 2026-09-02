using System.Net;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using MpApi.Core.Exceptions;
using MpApi.Core.Models;
using MpApi.Core.Options;
using MpApi.Core.Serialization;

namespace MpApi.Core.Http;

/// <summary>
/// Robust, resilient HTTP engine for Materials Project API requests.
/// </summary>
public sealed class MpHttpClient(HttpClient httpClient, MpApiOptions options)
    : IMpHttpClient
{
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private readonly MpApiOptions _options = options ?? throw new ArgumentNullException(nameof(options));

    public async Task<MpResponse<T>> GetAsync<T>(
        string endpoint,
        IReadOnlyDictionary<string, object?>? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        var uri = BuildUri(endpoint, queryParams);
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);

        return await ExecuteWithRetryAsync<MpResponse<T>>(request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<MpResponse<TResponse>> PostAsync<TRequest, TResponse>(
        string endpoint,
        TRequest body,
        IReadOnlyDictionary<string, object?>? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        var uri = BuildUri(endpoint, queryParams);
        using var request = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = JsonContent.Create(body, options: MpJsonSerializerOptions.Default)
        };

        return await ExecuteWithRetryAsync<MpResponse<TResponse>>(request, cancellationToken).ConfigureAwait(false);
    }

    public async IAsyncEnumerable<T> StreamAsync<T>(
        string endpoint,
        Dictionary<string, object?>? queryParams = null,
        int pageSize = 100,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        queryParams ??= [];
        var skip = 0;

        while (!cancellationToken.IsCancellationRequested)
        {
            queryParams["_limit"] = pageSize;
            queryParams["_skip"] = skip;

            var response = await GetAsync<T>(endpoint, queryParams, cancellationToken).ConfigureAwait(false);

            if (response.Data is null || response.Data.Count == 0)
                yield break;

            foreach (var item in response.Data)
            {
                yield return item;
            }

            if (response.Data.Count < pageSize)
                yield break;

            skip += pageSize;
        }
    }

    #region Execution & Resilience Logic

    private async Task<TResult> ExecuteWithRetryAsync<TResult>(
        HttpRequestMessage requestTemplate,
        CancellationToken cancellationToken)
    {
        var attempts = 0;
        var maxRetries = _options.MaxRetryCount;

        while (true)
        {
            attempts++;
            using var request = CloneHttpRequest(requestTemplate);

            try
            {
                using var response = await _httpClient.SendAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead,
                    cancellationToken).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    await using var contentStream = await response.Content
                        .ReadAsStreamAsync(cancellationToken)
                        .ConfigureAwait(false);

                    var result = await JsonSerializer.DeserializeAsync<TResult>(
                        contentStream,
                        MpJsonSerializerOptions.Default,
                        cancellationToken).ConfigureAwait(false);

                    return result ?? throw new MpApiException("The server returned an empty payload.");
                }

                // Handle HTTP errors
                await HandleErrorResponseAsync(response, cancellationToken).ConfigureAwait(false);
            }
            catch (MpApiRateLimitException ex) when (attempts <= maxRetries)
            {
                var delay = ex.RetryAfter ?? TimeSpan.FromSeconds(Math.Pow(2, attempts));
                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
            }
            catch (HttpRequestException) when (attempts <= maxRetries)
            {
                var delay = TimeSpan.FromSeconds(Math.Pow(2, attempts)) + TimeSpan.FromMilliseconds(Random.Shared.Next(50, 250));
                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
            }
        }
    }

    private static async Task HandleErrorResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var statusCode = (int)response.StatusCode;
        var errorBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        if (response.StatusCode == HttpStatusCode.TooManyRequests)
        {
            var retryAfter = response.Headers.RetryAfter?.Delta;
            throw new MpApiRateLimitException("Materials Project API Rate limit exceeded.", retryAfter, errorBody);
        }

        if (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
        {
            throw new MpApiAuthenticationException("Invalid or missing API key. Access denied.", statusCode, errorBody);
        }
        throw new MpApiException(
                $"API request to '{response.RequestMessage?.RequestUri}' failed with status code {statusCode}: {response.ReasonPhrase}. Response: {errorBody}",
                statusCode,
                errorBody);
    }
    private Uri BuildUri(string endpoint, IReadOnlyDictionary<string, object?>? queryParams)
    {
        var baseAddress = _options.BaseUrl.ToString().TrimEnd('/');
        var cleanEndpoint = endpoint.TrimStart('/');
        var url = $"{baseAddress}/{cleanEndpoint}";

        var effectiveParams = queryParams is not null
            ? new Dictionary<string, object?>(queryParams)
            : new Dictionary<string, object?>();

        // If specific fields are not requested, ask MP API for all fields
        if (!effectiveParams.ContainsKey("_fields") && !effectiveParams.ContainsKey("_all_fields"))
        {
            effectiveParams["_all_fields"] = "true";
        }

        var queryString = string.Join("&", effectiveParams
            .Where(kvp => kvp.Value is not null && !string.IsNullOrWhiteSpace(kvp.Value.ToString()))
            .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(FormatQueryValue(kvp.Value!))}"));

        return new Uri(string.IsNullOrEmpty(queryString) ? url : $"{url}?{queryString}");
    }
    private static string FormatQueryValue(object value) => value switch
    {
        bool b => b.ToString().ToLowerInvariant(),
        IEnumerable<string> list => string.Join(",", list),
        _ => value.ToString() ?? string.Empty
    };

    private static HttpRequestMessage CloneHttpRequest(HttpRequestMessage req)
    {
        var clone = new HttpRequestMessage(req.Method, req.RequestUri)
        {
            Content = req.Content,
            Version = req.Version
        };
        foreach (var header in req.Headers)
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

        return clone;
    }

    #endregion
}
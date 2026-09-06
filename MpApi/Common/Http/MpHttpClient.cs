using System.Net;
using System.Text.Json;
using MpApi.Common.Exceptions;
using MpApi.Common.Models;
using MpApi.Common.Serialization;

namespace MpApi.Common.Http;

/// <summary>
/// Default implementation of <see cref="IMpHttpClient"/> handling auth headers, rate limits, and error mapping.
/// </summary>
public sealed class MpHttpClient : IMpHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly MpApiOptions _options;

    public MpHttpClient(HttpClient httpClient, MpApiOptions options)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _options.Validate();

        ConfigureHttpClient();
    }

    private void ConfigureHttpClient()
    {
        _httpClient.BaseAddress = _options.BaseUrl;
        _httpClient.Timeout = _options.Timeout;

        _httpClient.DefaultRequestHeaders.Remove("X-API-KEY");
        _httpClient.DefaultRequestHeaders.Add("X-API-KEY", _options.ApiKey);

        if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "MpApiClient.NET/1.0");
        }
    }

    public async Task<MpResponse<T>> GetAsync<T>(
        string endpoint,
        IDictionary<string, string?>? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        var uri = BuildUri(endpoint, queryParams);

        using var response = await _httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);

        await EnsureSuccessStatusCodeAsync(response, cancellationToken).ConfigureAwait(false);

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        var result = await JsonSerializer.DeserializeAsync<MpResponse<T>>(stream, MpJsonSerializer.Options, cancellationToken)
            .ConfigureAwait(false);

        return result ?? throw new MpApiException("Received null response from the Materials Project API.");
    }

    public async Task<string> GetRawStringAsync(
        string endpoint,
        IDictionary<string, string?>? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        var uri = BuildUri(endpoint, queryParams);

        using var response = await _httpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
        await EnsureSuccessStatusCodeAsync(response, cancellationToken).ConfigureAwait(false);

        return await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
    }

    private static string BuildUri(string endpoint, IDictionary<string, string?>? queryParams)
    {
        if (queryParams == null || queryParams.Count == 0)
        {
            return endpoint;
        }

        var activeParams = queryParams
            .Where(kvp => !string.IsNullOrWhiteSpace(kvp.Value))
            .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value!)}");

        var queryString = string.Join("&", activeParams);
        return string.IsNullOrEmpty(queryString) ? endpoint : $"{endpoint}?{queryString}";
    }

    private static async Task EnsureSuccessStatusCodeAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        throw response.StatusCode switch
        {
            HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden =>
                new MpApiAuthenticationException(response.StatusCode, "Invalid or missing Materials Project API Key.", content),

            HttpStatusCode.NotFound =>
                new MpApiNotFoundException("The requested material or resource was not found.", content),

            HttpStatusCode.TooManyRequests =>
                new MpApiRateLimitException("API rate limit exceeded. Please throttle your requests.", response.Headers.RetryAfter?.Delta, content),

            _ => new MpApiHttpException(response.StatusCode, $"API request failed with status code {(int)response.StatusCode} ({response.StatusCode}).", content)
        };
    }
}
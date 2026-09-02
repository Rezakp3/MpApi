using MpApi.Core.Models;

namespace MpApi.Core.Http;

/// <summary>
/// Core HTTP transport engine for interacting with Materials Project API endpoints.
/// </summary>
public interface IMpHttpClient
{
    /// <summary>
    /// Sends a GET request and deserializes the response envelope.
    /// </summary>
    Task<MpResponse<T>> GetAsync<T>(
        string endpoint,
        IReadOnlyDictionary<string, object?>? queryParams = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a POST request with a JSON body and deserializes the response envelope.
    /// </summary>
    Task<MpResponse<TResponse>> PostAsync<TRequest, TResponse>(
        string endpoint,
        TRequest body,
        IReadOnlyDictionary<string, object?>? queryParams = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Automatically handles pagination and streams data items continuously.
    /// </summary>
    IAsyncEnumerable<T> StreamAsync<T>(
        string endpoint,
        Dictionary<string, object?>? queryParams = null,
        int pageSize = 100,
        CancellationToken cancellationToken = default);
}
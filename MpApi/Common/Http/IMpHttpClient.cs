using MpApi.Common.Models;

namespace MpApi.Common.Http;

/// <summary>
/// Internal HTTP client abstraction for dispatching requests to the Materials Project API.
/// </summary>
public interface IMpHttpClient
{
    /// <summary>
    /// Sends a GET request and deserializes the JSON envelope data payload.
    /// </summary>
    /// <typeparam name="T">Expected data payload type inside the response envelope.</typeparam>
    /// <param name="endpoint">Relative endpoint path (e.g. "materials/summary/").</param>
    /// <param name="queryParams">Query parameters to append to the URL.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Deserialized API response envelope.</returns>
    Task<MpResponse<T>> GetAsync<T>(
        string endpoint,
        IDictionary<string, string?>? queryParams = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a GET request expecting raw string content (e.g. CIF, POSCAR files).
    /// </summary>
    Task<string> GetRawStringAsync(
        string endpoint,
        IDictionary<string, string?>? queryParams = null,
        CancellationToken cancellationToken = default);
}
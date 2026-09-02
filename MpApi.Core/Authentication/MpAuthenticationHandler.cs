using System.Net.Http.Headers;
using MpApi.Core.Options;

namespace MpApi.Core.Authentication;

/// <summary>
/// Automatically attaches Materials Project API key and user agent headers to outgoing requests.
/// </summary>
public sealed class MpAuthenticationHandler(MpApiOptions options) : DelegatingHandler
{
    private const string ApiKeyHeaderName = "X-API-KEY";
    private readonly MpApiOptions _options = options ?? throw new ArgumentNullException(nameof(options));

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Inject API Key
        if (!string.IsNullOrWhiteSpace(_options.ApiKey))
        {
            request.Headers.Remove(ApiKeyHeaderName);
            request.Headers.Add(ApiKeyHeaderName, _options.ApiKey);
        }

        // Inject User-Agent
        if (!string.IsNullOrWhiteSpace(_options.UserAgent))
        {
            request.Headers.UserAgent.Clear();
            request.Headers.UserAgent.ParseAdd(_options.UserAgent);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
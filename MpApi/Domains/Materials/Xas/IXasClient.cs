using MpApi.Common.Models;
using MpApi.Domains.Materials.Xas.Models;

namespace MpApi.Domains.Materials.Xas;

public interface IXasClient
{
    Task<MpResponse<IReadOnlyList<XasDoc>>> SearchAsync(string materialId, CancellationToken cancellationToken = default);
}

namespace MpApi.Domains.Materials.Chemenv;

public interface IChemenvClient
{
    Task<ChemenvDoc?> GetByIdAsync(string materialId, CancellationToken cancellationToken = default);
}

using MpApi.Domains.Molecules.Absorption.Models;

namespace MpApi.Domains.Molecules.Absorption;

public interface IMoleculeAbsorptionClient
{
    Task<MoleculeAbsorptionDoc?> GetByIdAsync(string moleculeId, CancellationToken cancellationToken = default);
}

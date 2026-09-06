using MpApi.Domains.Applications;
using MpApi.Domains.Electronic;
using MpApi.Domains.Materials;
using MpApi.Domains.Mechanical;
using MpApi.Domains.Molecules;
using MpApi.Domains.Thermodynamics;

namespace MpApi;

/// <summary>
/// Root entry point interface for interacting with the Materials Project REST API.
/// </summary>
public interface IMpApiClient
{
    /// <summary>
    /// Crystallographic structures, property summaries, tasks, similarity, and chemical environments.
    /// </summary>
    IMaterialsDomain Materials { get; }

    /// <summary>
    /// Thermodynamic stability, formation energies, hull calculations, and phase diagrams.
    /// </summary>
    IThermodynamicsDomain Thermodynamics { get; }

    /// <summary>
    /// Electronic band structures, DOS, band gaps, dielectric tensors, and magnetism.
    /// </summary>
    IElectronicDomain Electronic { get; }

    /// <summary>
    /// Mechanical elasticity, piezoelectricity, and phonon vibrational dynamics.
    /// </summary>
    IMechanicalDomain Mechanical { get; }

    /// <summary>
    /// Industry applications including batteries, substrates, surfaces, alloys, and synthesis recipes.
    /// </summary>
    IApplicationsDomain Applications { get; }

    /// <summary>
    /// Non-periodic molecular properties, redox potentials, thermo, and absorption.
    /// </summary>
    IMoleculesDomain Molecules { get; }
}
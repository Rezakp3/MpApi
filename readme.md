# 🔬 MpApi.NET

[![NuGet Version](https://img.shields.io/nuget/v/MpApi.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/MpApi/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/MpApi.svg?style=flat-square)](https://www.nuget.org/packages/MpApi/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=flat-square)](LICENSE)
[![.NET Compatibility](https://img.shields.io/badge/.NET-8.0%20%7C%209.0%20%7C%2010.0-512BD4.svg?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)

A modern, high-performance, strongly-typed async C# client for the **[Materials Project REST API (v2)](https://materialsproject.org)**.

---

## 💡 The Story Behind MpApi

Like many open-source projects, **MpApi** was born out of personal necessity. While working on a materials science project in .NET, I found myself repeatedly writing boilerplate HTTP logic to query the Materials Project API. Rather than keeping this solution local, I decided to build a comprehensive, clean, and fully-typed .NET SDK and open-source it so no one else in the community has to reinvent the wheel from scratch.

> ⚡ **Built with AI & Vibe Coding:**  
> This library was architected and crafted leveraging modern **AI pair-programming and the "vibe coding" philosophy**. By combining state-of-the-art AI architectural capabilities with strict software engineering standards (Clean Code, SOLID, Domain-Driven Design), we built a fully resilient, production-ready SDK across 20+ scientific endpoints in record time.

---

## ✨ Features

- **🎯 Complete Domain Coverage:** Full support for Materials, Thermodynamics, Electronic structures, Mechanical properties, Industry Applications, and Molecules.
- **🧱 Clean Domain-Driven Architecture:** Logically grouped sub-clients (`client.Materials`, `client.Thermodynamics`, `client.Electronic`, etc.) rather than a cluttered flat client.
- **⚡ High-Performance Serialization:** Optimized with `System.Text.Json` source configuration for low allocations and blazing speed.
- **🛡️ Built-in Resilience:** First-class handling of API Rate Limiting (`HTTP 429`), network retries, and strongly-typed exceptions.
- **🔄 Dual Consumption Modes:** Instant single-line instantiation for Desktop/Console apps and full `IServiceCollection` / `IHttpClientFactory` integration for Web & Cloud apps.
- **🌐 Multi-Target Support:** Fully supports `.NET 8.0 (LTS)`, `.NET 9.0`, and `.NET 10.0`.

---

## 📦 Installation

Install via the .NET CLI:

```bash
dotnet add package MpApi
Or via the NuGet Package Manager Console in Visual Studio:
code
Powershell
Install-Package MpApi
🚀 Quickstart
1. Direct Usage (Console, Desktop, WPF, Avalonia, MAUI)
code
C#
using MpApi;
using MpApi.Domains.Materials.Summary.Models;

// 1. Initialize client with your Materials Project API Key
using var client = new MpApiClient("YOUR_API_KEY");

// 2. Query summary properties of a material (e.g. Silicon)
var silicon = await client.Materials.Summary.GetByIdAsync("mp-149");

Console.WriteLine($"Formula: {silicon?.FormulaPretty}");
Console.WriteLine($"Band Gap: {silicon?.BandGap} eV");
Console.WriteLine($"Is Stable: {silicon?.IsStable}");
2. Dependency Injection (ASP.NET Core, Worker Services)
Register the client in your Program.cs:
code
C#
using MpApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register MpApiClient with IHttpClientFactory lifecycle
builder.Services.AddMpApiClient(options =>
{
    options.ApiKey = builder.Configuration["MaterialsProject:ApiKey"]!;
    options.Timeout = TimeSpan.FromSeconds(45);
});

var app = builder.Build();
Inject IMpApiClient wherever you need it:
code
C#
public class MaterialsService(IMpApiClient mpClient)
{
    public async Task<double?> GetBandGapAsync(string materialId)
    {
        var doc = await mpClient.Electronic.BandGap.GetByIdAsync(materialId);
        return doc?.BandGap;
    }
}
🧪 Domain Examples
📊 1. Thermodynamics & Phase Diagram Data
Query all competing thermodynamic entries within a chemical system to construct a Convex Hull / Phase Diagram:
code
C#
// Fetch all phase entries for the Li-Fe-O ternary system
var entries = await client.Thermodynamics.Thermo.GetPhaseDiagramEntriesAsync("Li-Fe-O");

foreach (var phase in entries.Where(p => p.IsStable))
{
    Console.WriteLine($"Stable Phase: {phase.FormulaPretty} (E_above_hull: {phase.EnergyAboveHull} eV/atom)");
}
🔋 2. Batteries & Insertion Electrodes
Find high-performance battery cathode candidates:
code
C#
using MpApi.Domains.Applications.Batteries.Models;

var filter = new BatterySearchFilter
{
    WorkingIon = "Li",
    AverageVoltageMin = 3.5,
    MaxFracVolumeChange = 0.08, // Less than 8% volume expansion
    Limit = 10
};

var batteryResponse = await client.Applications.Batteries.SearchAsync(filter);

foreach (var battery in batteryResponse.Data ?? [])
{
    Console.WriteLine($"Battery: {battery.BatteryFormula} | Voltage: {battery.AverageVoltage} V | Capacity: {battery.GravimetricCapacity} mAh/g");
}
📐 3. Mechanical & Elastic Moduli
Retrieve 6x6 stiffness tensors and bulk/shear moduli:
code
C#
var elasticity = await client.Mechanical.Elasticity.GetByIdAsync("mp-149");

Console.WriteLine($"Bulk Modulus (VRH): {elasticity?.KVrh} GPa");
Console.WriteLine($"Shear Modulus (VRH): {elasticity?.GVrh} GPa");
Console.WriteLine($"Poisson's Ratio: {elasticity?.HomogeneousPoisson}");
🧬 4. Non-Periodic Molecules
Query molecular properties, orbital energies, and redox potentials:
code
C#
var molecule = await client.Molecules.Summary.GetByIdAsync("mol-12345");

Console.WriteLine($"Formula: {molecule?.FormulaPretty}");
Console.WriteLine($"HOMO-LUMO Gap: {molecule?.Gap} eV");
Console.WriteLine($"Ionization Energy: {molecule?.IonizationEnergy} eV");
🗺️ Architectural Domain Map
code
Text
IMpApiClient
│
├── Materials          --> Summary, Structures (CIF/POSCAR), Tasks, Similarity, Chemenv, Robocrys, XAS
├── Thermodynamics     --> Thermo Stability, Hull Energies, Phase Diagram Data
├── Electronic         --> BandStructure, BandGap (GGA/HSE06), Dielectric Tensors, Magnetism
├── Mechanical         --> Elasticity (6x6 Tensors), Piezoelectric, Phonon Dynamics
├── Applications       --> Batteries, Epitaxial Substrates, Surfaces (Wulff), Alloys, Synthesis Recipes
└── Molecules          --> Molecular Summary, Thermo, Redox Potentials, Optical Absorption, Tasks
🤝 Contributing
Contributions, bug reports, and feature requests are very welcome!
Feel free to open an Issue or submit a Pull Request.
Fork the repository
Create your feature branch (git checkout -b feature/AmazingFeature)
Commit your changes (git commit -m 'Add some AmazingFeature')
Push to the branch (git push origin feature/AmazingFeature)
Open a Pull Request
📜 License
This project is licensed under the MIT License - see the LICENSE file for details.
<p align="center">
Crafted with ❤️ and Vibe Coding by <a href="https://github.com/Rezakp3">Rezakp3</a> and the open-source community.
</p>
```
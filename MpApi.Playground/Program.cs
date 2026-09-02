using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MpApi;
using MpApi.DependencyInjection;
using MpApi.Extensions.DependencyInjection;
using MpApi.Materials;
using MpApi.Materials.Models;
using MpApi.Materials.Utilities;

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("==================================================");
Console.WriteLine("        Welcome to MpApi.NET Playground           ");
Console.WriteLine("==================================================");
Console.ResetColor();

// Read API Key from environment variable or prompt user input
var apiKey = Environment.GetEnvironmentVariable("MP_API_KEY") ?? "YOUR_API_KEY_HERE";

if (apiKey == "YOUR_API_KEY_HERE")
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("Please enter your Materials Project API Key: ");
    apiKey = Console.ReadLine()?.Trim() ?? string.Empty;
    Console.ResetColor();
}

// ---------------------------------------------------------
// Scenario 1: Standalone Facade Client Usage (Desktop / CLI apps)
// ---------------------------------------------------------
Console.WriteLine("\n[1] Testing Standalone Facade Client...");
using var client = new MpApiClient(apiKey);

// Fetch Silicon crystal summary (mp-149)
Console.WriteLine("Fetching Silicon (mp-149)...");
var silicon = await client.Materials.GetByIdAsync("mp-149");

if (silicon is not null)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"   Formula:        {silicon.FormulaPretty}");
    Console.WriteLine($"   Band Gap:       {silicon.BandGap} eV (Direct: {silicon.IsGapDirect})");
    Console.WriteLine($"   Crystal System: {silicon.Symmetry?.CrystalSystem}");
    Console.WriteLine($"   Density:        {silicon.Density:F2} g/cm³");
    Console.WriteLine($"   Is Stable:      {silicon.IsStable}");
    Console.ResetColor();
}

// ---------------------------------------------------------
// Scenario 2: Fetching 3D Crystal Structure & Generating CIF String
// ---------------------------------------------------------
Console.WriteLine("\n[2] Fetching 3D Crystal Structure and generating CIF export...");
var structure = await client.Materials.GetStructureAsync("mp-149");

if (structure is not null)
{
    var cifContent = structure.ToCif(title: "Silicon_mp-149");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("Generated CIF Preview (First 10 lines):");
    Console.WriteLine(string.Join(Environment.NewLine, cifContent.Split(Environment.NewLine).Take(10)));
    Console.ResetColor();
}

// ---------------------------------------------------------
// Scenario 3: Memory-Efficient Async Streaming (IAsyncEnumerable)
// ---------------------------------------------------------
Console.WriteLine("\n[3] Streaming Stable Semiconductors (BandGap: 1.1 - 1.8 eV)...");

var filter = new SummarySearchCriteria
{
    BandGap = (Min: 1.1, Max: 1.8),
    IsStable = true,
    IsMetal = false
};

var count = 0;
await foreach (var mat in client.Materials.StreamSearchAsync(filter, pageSize: 5))
{
    Console.WriteLine($"   -> Found #{++count}: {mat.FormulaPretty,-10} (ID: {mat.MaterialId,-10}, Eg: {mat.BandGap:F2} eV)");
    if (count >= 5) break; // Displaying first 5 results for demonstration
}

// ---------------------------------------------------------
// Scenario 4: Querying Thermodynamic Phase Data for a Chemical System
// ---------------------------------------------------------
Console.WriteLine("\n[4] Querying Thermodynamic Phase Data for Fe-O system...");
var thermoEntries = await client.Thermo.GetByChemsysAsync("Fe-C");

if (thermoEntries.Count > 0)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine($"   Found {thermoEntries.Count} thermodynamic entries for Fe-O:");
    foreach (var entry in thermoEntries.Take(5))
    {
        Console.WriteLine($"      - ID: {entry.MaterialId,-10} | Formula: {entry.FormulaPretty,-8} | Formation Energy: {entry.FormationEnergyPerAtom:F3} eV/atom | Stable: {entry.IsStable}");
    }
    Console.ResetColor();
}

// ---------------------------------------------------------
// Scenario 5: ASP.NET Core & Host Dependency Injection Setup
// ---------------------------------------------------------
Console.WriteLine("\n[5] Testing Dependency Injection Registration...");

var builder = Host.CreateApplicationBuilder();
builder.Services.AddMpApi(options =>
{
    options.ApiKey = apiKey;
    options.Timeout = TimeSpan.FromSeconds(15);
});

using var host = builder.Build();

// Resolve registered service from the DI container
var diMaterialsClient = host.Services.GetRequiredService<IMpMaterialsClient>();
var testMaterial = await diMaterialsClient.GetByIdAsync("mp-13"); // Iron

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"   DI Resolved Material: {testMaterial?.FormulaPretty} ({testMaterial?.MaterialId})");
Console.ResetColor();

Console.WriteLine("\nAll playground tests completed successfully!");
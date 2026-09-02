using System.Globalization;
using System.Text;
using MpApi.Materials.Models;

namespace MpApi.Materials.Utilities;

/// <summary>
/// Generates standard Crystallographic Information File (.cif) strings from CrystalStructure models.
/// </summary>
public static class CifWriter
{
    public static string ToCif(this CrystalStructure structure, string? title = null)
    {
        ArgumentNullException.ThrowIfNull(structure);

        var sb = new StringBuilder();
        var lat = structure.Lattice;
        var culture = CultureInfo.InvariantCulture;

        sb.AppendLine($"data_{title ?? "crystal"}");
        sb.AppendLine("_audit_creation_method   'MpApi.NET'");
        sb.AppendLine();

        // Lattice parameters
        sb.AppendLine($"_cell_length_a           {lat.A.ToString("F6", culture)}");
        sb.AppendLine($"_cell_length_b           {lat.B.ToString("F6", culture)}");
        sb.AppendLine($"_cell_length_c           {lat.C.ToString("F6", culture)}");
        sb.AppendLine($"_cell_angle_alpha        {lat.Alpha.ToString("F6", culture)}");
        sb.AppendLine($"_cell_angle_beta         {lat.Beta.ToString("F6", culture)}");
        sb.AppendLine($"_cell_angle_gamma        {lat.Gamma.ToString("F6", culture)}");
        sb.AppendLine($"_cell_volume             {lat.Volume.ToString("F6", culture)}");
        sb.AppendLine();

        // Symmetry
        sb.AppendLine("_symmetry_space_group_name_H-M   'P 1'");
        sb.AppendLine("_symmetry_Int_Tables_number       1");
        sb.AppendLine();

        // Sites loop
        sb.AppendLine("loop_");
        sb.AppendLine(" _atom_site_label");
        sb.AppendLine(" _atom_site_type_symbol");
        sb.AppendLine(" _atom_site_fract_x");
        sb.AppendLine(" _atom_site_fract_y");
        sb.AppendLine(" _atom_site_fract_z");
        sb.AppendLine(" _atom_site_occupancy");

        var siteIndex = 1;
        foreach (var site in structure.Sites)
        {
            var element = site.Species.FirstOrDefault()?.Element ?? site.Label ?? "X";
            var occu = site.Species.FirstOrDefault()?.Occupancy ?? 1.0;
            var x = site.FractionalCoordinates.ElementAtOrDefault(0);
            var y = site.FractionalCoordinates.ElementAtOrDefault(1);
            var z = site.FractionalCoordinates.ElementAtOrDefault(2);

            sb.AppendLine(string.Format(
                culture,
                " {0,-6} {1,-4} {2,10:F6} {3,10:F6} {4,10:F6} {5,6:F2}",
                $"{element}{siteIndex++}",
                element,
                x, y, z, occu));
        }

        return sb.ToString();
    }
}
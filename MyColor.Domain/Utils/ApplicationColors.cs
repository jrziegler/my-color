using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyColor.Domain.Entities;
using MyColor.Domain.Validation;

namespace MyColor.Domain.Utils
{
    public static class ApplicationColors
    {
        public static readonly IList<Color> ListOfColors = new ReadOnlyCollection<Color>
        (new[] {
             new Color(1, "blau"),
             new Color(2, "grün"),
             new Color(3, "violett"),
             new Color(4, "rot"),
             new Color(5, "gelb"),
             new Color(6, "türkis"),
             new Color(7, "weiß")
        });
        
        public static string ListColors()
        {
            string colors = string.Empty;
            foreach (Color c in ListOfColors)
                colors = string.Concat(colors, $"{c.Name} ");
            
            return colors;
        }

        public static string GetColorNameById(int id)
        {
            Color? color = ListOfColors.FirstOrDefault(x => x.Id == id);
            return color?.Name ?? string.Empty;
        }

        public static int GetColorIdByName(string name)
        {
            Color? color = ListOfColors.FirstOrDefault(x => x.Name == name.ToLower());
            return color?.Id ?? 0;
        }

        public static bool ContainColor(int id)
        {
            return ListOfColors.Any(x => x.Id == id);
        }
    }
}

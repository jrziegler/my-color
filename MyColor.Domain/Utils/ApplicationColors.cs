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
             new Color(5, "geld"),
             new Color(6, "türkis"),
             new Color(7, "weiß")
        });
        
        private static string listColors()
        {
            string colors = string.Empty;
            foreach (Color c in ListOfColors)
                colors = string.Concat(colors, $"[{c.Name}]");
            
            return colors;
        }

        public static string GetColorNameById(int id)
        {
            Color? color = ListOfColors.FirstOrDefault(x => x.Id == id);
            DomainExceptionValidation.When(color == null, $"Color with id {id} not found."); 

            return color?.Name;
        }

        public static int GetColorIdByName(string name)
        {
            Color? color = ListOfColors.FirstOrDefault(x => x.Name == name.ToLower());
            DomainExceptionValidation.When(color?.Name == null, $"Color with name {name} does not exist. The colors are: {listColors()}");

            return (int)color?.Id;
        }
    }
}

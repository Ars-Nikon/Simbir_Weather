using System.IO;
using Simbirsoft_Weather.Models;
using Simbirsoft_Weather.Models.Enums;

namespace Simbirsoft_Weather.Services
{
    public class RecPatternWritter : IRecPatternWritter
    {
        public string WriteRec(Clothes clothes, ForWhom forWhom)
        {
            string text = File.ReadAllText(Directory.GetCurrentDirectory() + "/Resources/clothesRecPattern.html");
            string gender = forWhom == ForWhom.Man ? "men" : "women";
            return string.Format(text, clothes.Name, gender);
        }
    }
}

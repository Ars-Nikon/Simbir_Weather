using Simbirsoft_Weather.Models;
using Simbirsoft_Weather.Models.Enums;

namespace Simbirsoft_Weather.Services
{
    public interface IRecPatternWritter
    {
        string WriteRec(Clothes clothes, ForWhom forWhom);
    }
}

using Simbirsoft_Weather.Models;
using System;
using System.Collections.Generic;

namespace Simbirsoft_Weather.Services
{
    public interface IClothesRepository : IDisposable
    {
        IEnumerable<Clothes> GetClothes();
    }
}

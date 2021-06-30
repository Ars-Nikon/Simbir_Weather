using System;
using Simbirsoft_Weather.Models;
using System.Collections.Generic;

namespace Simbirsoft_Weather.Services
{
    public interface IClothesRepository : IDisposable
    {
        IEnumerable<Clothes> GetClothes();
    }
}

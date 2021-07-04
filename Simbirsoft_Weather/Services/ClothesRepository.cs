using Microsoft.EntityFrameworkCore;
using Simbirsoft_Weather.Models;
using Simbirsoft_Weather.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simbirsoft_Weather.Services
{
    public class ClothesRepository : IClothesRepository
    {
        private readonly ClothesContext _db;

        public ClothesRepository(DbContextOptions<ClothesContext> options)
        {
            _db = new ClothesContext(options);
        }

        public IEnumerable<Clothes> GetClothes()
        {
            return _db.Clothes.AsEnumerable();
        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}

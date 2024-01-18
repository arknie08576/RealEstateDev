using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateDev.Data;
using RealEstateDev.Entities;


namespace RealEstateDev.Data
{
    public class RealEstateDevDbContex : DbContext
    {
        public DbSet<RealEstate> RealEstates => Set<RealEstate>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("StorageAppDb");
        }
    }
}

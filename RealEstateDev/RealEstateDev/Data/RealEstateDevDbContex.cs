using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateDev.Data;
using RealEstateDev.Entities;
using RealEstateDev.Components.CsvReader.Models;

namespace RealEstateDev.Data
{
    public class RealEstateDevDbContex : DbContext
    {
        public RealEstateDevDbContex(DbContextOptions<RealEstateDevDbContex> options):base(options) 
        {
            
        }
        public DbSet<House> Houses { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        //public DbSet<RealEstate> RealEstates => Set<RealEstate>();
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseInMemoryDatabase("StorageAppDb");
        //}
    }
}

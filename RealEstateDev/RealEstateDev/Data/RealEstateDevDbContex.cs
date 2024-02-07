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

    }
}

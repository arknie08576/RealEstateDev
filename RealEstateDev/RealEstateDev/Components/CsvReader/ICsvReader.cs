using RealEstateDev.Components.CsvReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDev.Components.CsvReader
{
    public interface ICsvReader
    {
        List<Car> ProcessCars(string filepath);
        List<Manufacturer> ProcessManufacturers(string filepath);
        public void Reader();
    }
}

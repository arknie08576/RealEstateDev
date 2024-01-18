using RealEstateDev.Components.CsvReader.Extensions;
using RealEstateDev.Components.CsvReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDev.Components.CsvReader
{
    public class CsvReader : ICsvReader
    {
        public List<Car> ProcessCars(string filepath)
        {
            if (!File.Exists(filepath))
            {
                return new List<Car>();

            }
            else
            {

                var cars = File.ReadAllLines(filepath).Skip(1).Where(x => x.Length > 1).ToCar();

                return cars.ToList();

            }
        }

        public List<Manufacturer> ProcessManufacturers(string filepath)
        {
            if (!File.Exists(filepath))
            {
                return new List<Manufacturer>();

            }
            else
            {

                var manufacturers = File.ReadAllLines(filepath).Where(x => x.Length > 1).Select(x =>
                {
                    var columns = x.Split(',');
                    return new Manufacturer()
                    {
                        Name = columns[0],
                        Country = columns[1],
                        Year = int.Parse(columns[2])

                    };

                });

                return manufacturers.ToList();

            }
        }



        public void Reader()
        {
            var cars = ProcessCars("Resources\\Files\\fuel.csv");
            var manufacturers = ProcessManufacturers("Resources\\Files\\manufacturers.csv");
            var groups1 = cars.GroupBy(c => c.Manufacturer);
            var groups = cars.GroupBy(c => c.Manufacturer).Select(g => new
            {
                Name = g.Key,
                Max = g.Max(c => c.Combined),
                Average = g.Average(c => c.Combined)
            }).OrderBy(x => x.Average);

            //foreach (var group in groups)
            //{
            //    Console.WriteLine($"{group.Name}");
            //    Console.WriteLine($"Max: {group.Max}");
            //    Console.WriteLine($"Average: {group.Average}");
            //}

            var carsInCountry = cars.Join(manufacturers, x => new { x.Manufacturer, x.Year }, x => new { Manufacturer = x.Name, x.Year }, (car, manufacturer) => new
            {
                manufacturer.Country,
                car.Name,
                car.Combined


            }).OrderByDescending(x => x.Combined).ThenBy(x => x.Name);

            //foreach (var car in carsInCountry)
            //{
            //    Console.WriteLine($"Country: {car.Country}");
            //    Console.WriteLine($"\tName: {car.Name}");
            //    Console.WriteLine($"\tCombined: {car.Combined}");


            //}

            var grupy = manufacturers.GroupJoin(cars, manufacturer => manufacturer.Name, car => car.Manufacturer, (m, g) => new
            {
                Manufacturer = m,
                Cars = g

            }).OrderBy(x => x.Manufacturer.Name);
            //foreach (var group in grupy)
            //{
            //    Console.WriteLine($"Manufacturer: {group.Manufacturer.Name}");
            //    Console.WriteLine($"\tCars: {group.Cars.Count()}");
            //    Console.WriteLine($"\tMax: {group.Cars.Max(x => x.Combined)}");
            //    Console.WriteLine($"\tMin: {group.Cars.Min(x => x.Combined)}");
            //    Console.WriteLine($"\tAvg: {group.Cars.Average(x => x.Combined)}");
            //    Console.WriteLine();


            //}


        }
    }



}

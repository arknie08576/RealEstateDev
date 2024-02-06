using RealEstateDev.Components.CsvReader;
using RealEstateDev.Components.CsvReader.Models;
using RealEstateDev.Components.XmlCreator;
using RealEstateDev.Data;
using RealEstateDev.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RealEstateDev
{
    public class App : IApp
    {
        private readonly IUserCommunication _userCommunication;
        private readonly ICsvReader _csvReader;
        private readonly IXmlCreator _xmlCreator;
        RealEstateDevDbContex _dbContex;

        public App(IUserCommunication userCommunication, ICsvReader csvReader, IXmlCreator xmlCreator, RealEstateDevDbContex dbContex)
        {
            _userCommunication = userCommunication;
            _csvReader = csvReader;
            _xmlCreator = xmlCreator;
            _dbContex = dbContex;
            _dbContex.Database.EnsureCreated();
        }
        public void Run()
        {
            // CreateXml();
            // QueryXml();
            // _csvReader.Reader();
            // InsertData();
            //ReadGroupedCarsFromDb();

            //  _xmlCreator.CreateTemplatedXml();
            _userCommunication.CommunicationWithUser();
        }
        //private void RemoveCar(string name)
        //{
        //    var car = this.ReadFirstOrDefault(name);
        //    _dbContex.Cars.Remove(car);
        //    _dbContex.SaveChanges();
        //}
        //private void ChangeName(string name)
        //{
        //    var car = this.ReadFirstOrDefault(name);
        //    car.Name = "Mój samochód";
        //    _dbContex.SaveChanges();

        //}

        //private Car? ReadFirstOrDefault(string name)
        //{
        //    return _dbContex.Cars.FirstOrDefault(x=>x.Name == name);

        //}
        //private void ReadGroupedCarsFromDb()
        //{

        //    var query = _dbContex.Cars.GroupBy(x => x.Manufacturer).
        //        Select(x => new
        //        {
        //            Name = x.Key,
        //            Cars=x.ToList()
        //        }).ToList();
        //    foreach(var group in query) {
        //        Console.WriteLine(group.Name);
        //        Console.WriteLine("=======");
        //        foreach(var car in group.Cars)
        //        {
        //            Console.WriteLine($"\t{car.Name}: {car.Combined}");
        //        }
        //        Console.WriteLine();


        //    }
        //}
        //private void ReadAllCarsFromDb()
        //{
        //    var carsFromDb = _dbContex.Cars.ToList();
        //}
        //private void InsertData()
        //{
        //    var cars = _csvReader.ProcessCars("Resources\\Files\\fuel.csv");
        //    foreach (var car in cars)
        //    {
        //        _dbContex.Cars.Add(new Car()
        //        {
        //            Manufacturer = car.Manufacturer,
        //            Name = car.Name,
        //            Year = car.Year,
        //            City = car.City,
        //            Combined = car.Combined,
        //            Cylinders = car.Cylinders,
        //            Displacement = car.Displacement,
        //            Highway = car.Highway

        //        });

        //    }
        //    _dbContex.SaveChanges();
        //}

        private void CreateXml()
        {
            var records = _csvReader.ProcessCars("Resources\\Files\\fuel.csv");

            var document = new XDocument();
            var cars = new XElement("Cars", records
                .Select(x => new XElement("Car",
                new XAttribute("Name", x.Name),
                new XAttribute("Combined", x.Combined),
                new XAttribute("Manufacturer", x.Manufacturer)
                )));
            document.Add(cars);
            document.Save("fuel.xml");
        }

        private void QueryXml()
        {

            var document = XDocument.Load("fuel.xml");
            var names = document
                .Element("Cars")?
                .Elements("Car")
                .Where(x => x.Attribute("Manufacturer")?.Value == "BMW")
                .Select(x => x.Attribute("Name")?.Value);

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}

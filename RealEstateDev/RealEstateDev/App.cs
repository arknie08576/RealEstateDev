﻿using RealEstateDev.Components.CsvReader;
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
        RealEstateDevDbContex _dbContex;

        public App(IUserCommunication userCommunication, ICsvReader csvReader, RealEstateDevDbContex dbContex)
        {
            _userCommunication = userCommunication;
            _csvReader = csvReader;
            _dbContex = dbContex;
            _dbContex.Database.EnsureCreated();
        }
        public void Run()
        {
            _userCommunication.CommunicationWithUser();
        }
       
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

using RealEstateDev.Components.CsvReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RealEstateDev.Components.XmlCreator
{
    public class XmlCreator : IXmlCreator
    {
        private readonly ICsvReader _csvReader;
        public XmlCreator(ICsvReader csvReader)
        {
            _csvReader = csvReader;
        }
        public void CreateTemplatedXml()
        {
            var records = _csvReader.ProcessCars("Resources\\Files\\fuel.csv");

            var cars = _csvReader.ProcessCars("Resources\\Files\\fuel.csv");
            var manufacturers = _csvReader.ProcessManufacturers("Resources\\Files\\manufacturers.csv");
            var grupy = manufacturers.GroupJoin(cars, manufacturer => manufacturer.Name, car => car.Manufacturer, (m, g) => new
            {
                Manufacturer = m.Name,
                Country = m.Country,
                CombinedSum = g.Sum(x => x.Combined),
                Cars = g
            }); ;
            var doc = new XDocument();
            var ele = new XElement("Manufacturers",
                grupy.Select(x => new XElement("Manufacturer",
                    new XAttribute("Name", x.Manufacturer),
                    new XAttribute("Country", x.Country),
                    new XElement("Cars",
                        new XAttribute("country", x.Country),
                        new XAttribute("CombinedSum", x.CombinedSum)
                    ,
                        cars.Where(y => y.Manufacturer == x.Manufacturer).Select(y => new XElement("Car", new XAttribute("Model", y.Name), new XAttribute("Combined", y.Combined))
                    ))
                ))
            );
            doc.Add(ele);
            doc.Save("fuel.xml");

        }
    }
}

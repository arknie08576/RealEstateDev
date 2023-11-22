using RealEstateDev.Entities;
using RealEstateDev.Repositories;
using RealEstateDev.Data;
using System.Drawing;
using System.Xml.Linq;

var sqlRepository = new SqlRepository<RealEstate>(new RealEstateDevDbContex());

sqlRepository.Add(new Apartment("Małe mieszkanie", 500000, 50, 3));
sqlRepository.Add(new Apartment("Duża kawalerka", 300000, 30, 2));
sqlRepository.Add(new Apartment("Duży apartament", 1000000, 150, 5));
sqlRepository.Add(new House("Średni dom", 1200000, 150, 300));
sqlRepository.Add(new House("Duży dom", 1500000, 190, 400));
sqlRepository.Save();

foreach (var item in sqlRepository.GetAll())
{
    Console.WriteLine(item.ToString());
}
using RealEstateDev.Entities;
using RealEstateDev.Repositories;
using RealEstateDev.Data;
using System.Drawing;
using System.Xml.Linq;

var fileRepository = new FileRepository<RealEstate>();
fileRepository.ItemAdded += RealEstateRepoOnItemAdded;
fileRepository.ItemRemoved += RealEstateRepoOnItemRemoved;


bool runProgram=true;
while (runProgram)
{
    PrintMenu();
    string x = "0";
    x = Console.ReadLine();
    switch (x)
    {
        case "1":
            foreach (var item in fileRepository.GetAll())
            {
                Console.WriteLine(item);

            }
            break;
        case "2":
            string y = "a";
            while (!(y == "d" || y == "m"))
            {
                Console.WriteLine("Wpisz d jeśli chcesz dodać dom lub m jeśli chcesz dodać mieszkanie");
            y = Console.ReadLine();
            }
            if (y == "d") //string name, int value, int area, int landArea
            {
                Console.WriteLine("Wpisz nazwę domu");
                string name= Console.ReadLine();
                Console.WriteLine("Wpisz wartość domu");
                int value =Int32.Parse(Console.ReadLine());
                Console.WriteLine("Wpisz powierzchnię domu");
                int area = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Wpisz powierzchnię działki domu");
                int landArea = Int32.Parse(Console.ReadLine());
                fileRepository.Add(new House(name,  value,  area,  landArea));

            }
            if (y == "m") //string name, int value, int area, int landArea
            {
                Console.WriteLine("Wpisz nazwę mieszkania");
                string name = Console.ReadLine();
                Console.WriteLine("Wpisz wartość mieszkania");
                int value = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Wpisz powierzchnię mieszkania");
                int area = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Wpisz piętro mieszkania");
                int floor = Int32.Parse(Console.ReadLine());
                fileRepository.Add(new Apartment(name, value, area, floor));

            }


            break; 
        case "3":
            Console.WriteLine("Wpisz id nieruchomości do usunięcia");
            int id = Int32.Parse(Console.ReadLine());
            var realEstate=fileRepository.GetById(id);
            if(realEstate != null)
            {
                fileRepository.Remove(realEstate);
            }
            else
            {
                Console.WriteLine("Wrong ID");
            }
            break;
        case "4":
            fileRepository.Save();
            runProgram = false;
            break;
        default:
            break;



    }
}






/*
fileRepository.Add(new Apartment("Małe mieszkanie", 500000, 50, 3));
fileRepository.Add(new Apartment("Duża kawalerka", 300000, 30, 2));
fileRepository.Add(new Apartment("Duży apartament", 1000000, 150, 5));
fileRepository.Add(new House("Średni dom", 1200000, 150, 300));
fileRepository.Add(new House("Duży dom", 1500000, 190, 400));
fileRepository.Save();

*/
void PrintMenu()
{
    Console.WriteLine("Witaj w programie do zarządzania nieruchomościami");
    Console.WriteLine("Wpisz 1 aby wyświetlić posiadane nieruchomości");
    Console.WriteLine("Wpisz 2 aby dodać nieruchomość");
    Console.WriteLine("Wpisz 3 aby usunąć nieruchomość");
    Console.WriteLine("Wpisz 4 aby zamknąć program");

}
static void RealEstateRepoOnItemAdded(object? sender, RealEstate item)
{
    string logfile = "log.txt";

    string s = ("[" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + "]-RealEstateAdded-[" + item.Name + "]");
    File.AppendAllText(logfile, s + Environment.NewLine);
}
static void RealEstateRepoOnItemRemoved(object? sender, RealEstate item)
{
    string logfile = "log.txt";

    string s = ("[" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + "]-RealEstateRemoved-[" + item.Name + "]");
    File.AppendAllText(logfile, s + Environment.NewLine);
}
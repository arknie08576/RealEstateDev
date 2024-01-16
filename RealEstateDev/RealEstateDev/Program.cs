using RealEstateDev.Entities;
using RealEstateDev.Repositories;
using RealEstateDev.Data;
using System.Drawing;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;
using RealEstateDev.Services;
using RealEstateDev;
using RealEstateDev.DataProviders;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<RealEstate>, FileRepository<RealEstate>>();
services.AddSingleton<IRealEstateProvider, RealEstateProvider>();
services.AddSingleton<IUserCommunication, UserCommunication>();
var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();
//var fileRepository = new FileRepository<RealEstate>();
//fileRepository.ItemAdded += RealEstateRepoOnItemAdded;
//fileRepository.ItemRemoved += RealEstateRepoOnItemRemoved;

//bool runprogram = true;
//while (runprogram)
//{
//    printmenu();
//    string x = "0";
//    x = console.readline();
//    switch (x)
//    {
//        case "1":
//            foreach (var item in filerepository.getall())
//            {
//                console.writeline(item);
//            }
//            break;
//        case "2":
//            string y = "a";
//            while (!(y == "d" || y == "m"))
//            {
//                console.writeline("wpisz d jeśli chcesz dodać dom lub m jeśli chcesz dodać mieszkanie");
//                y = console.readline();
//            }
//            if (y == "d")
//            {
//                console.writeline("wpisz nazwę domu");
//                string name = console.readline();
//                console.writeline("wpisz wartość domu");
//                int value = int32.parse(console.readline());
//                console.writeline("wpisz powierzchnię domu");
//                int area = int32.parse(console.readline());
//                console.writeline("wpisz powierzchnię działki domu");
//                int landarea = int32.parse(console.readline());
//                filerepository.add(new house(name, value, area, landarea));
//            }
//            if (y == "m")
//            {
//                console.writeline("wpisz nazwę mieszkania");
//                string name = console.readline();
//                console.writeline("wpisz wartość mieszkania");
//                int value = int32.parse(console.readline());
//                console.writeline("wpisz powierzchnię mieszkania");
//                int area = int32.parse(console.readline());
//                console.writeline("wpisz piętro mieszkania");
//                int floor = int32.parse(console.readline());
//                filerepository.add(new apartment(name, value, area, floor));
//            }
//            break;
//        case "3":
//            console.writeline("wpisz id nieruchomości do usunięcia");
//            int id = int32.parse(console.readline());
//            var realestate = filerepository.getbyid(id);
//            if (realestate != null)
//            {
//                filerepository.remove(realestate);
//            }
//            else
//            {
//                console.writeline("wrong id");
//            }
//            break;
//        case "4":
//            filerepository.save();
//            runprogram = false;
//            break;
//        default:
//            break;
//    }
//}
//void printmenu()
//{
//    console.writeline("witaj w programie do zarządzania nieruchomościami");
//    console.writeline("wpisz 1 aby wyświetlić posiadane nieruchomości");
//    console.writeline("wpisz 2 aby dodać nieruchomość");
//    console.writeline("wpisz 3 aby usunąć nieruchomość");
//    console.writeline("wpisz 4 aby zamknąć program");

//}
//static void realestaterepoonitemadded(object? sender, realestate item)
//{
//    string logfile = "log.txt";

//    string s = ("[" + datetime.now.tostring("yyyy-mm-dd_hh-mm-ss") + "]-realestateadded-[" + item.name + "]");
//    file.appendalltext(logfile, s + environment.newline);
//}
//static void realestaterepoonitemremoved(object? sender, realestate item)
//{
//    string logfile = "log.txt";

//    string s = ("[" + datetime.now.tostring("yyyy-mm-dd_hh-mm-ss") + "]-realestateremoved-[" + item.name + "]");
//    file.appendalltext(logfile, s + environment.newline);
//}
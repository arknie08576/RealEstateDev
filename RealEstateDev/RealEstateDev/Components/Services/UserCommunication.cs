using RealEstateDev.Components.DataProviders;
using RealEstateDev.Data;
using RealEstateDev.DataProviders;
using RealEstateDev.Entities;
using RealEstateDev.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDev.Services
{
    public class UserCommunication : IUserCommunication
    {
        private readonly IRepository<RealEstate> _reRepository;
        private readonly IRealEstateProvider _reProvider;
        public UserCommunication(IRepository<RealEstate> reRepository, IRealEstateProvider reProvider) {
            _reRepository=reRepository;
            _reProvider=reProvider;
            _reRepository.ItemAdded += RealEstateRepoOnItemAdded;
            _reRepository.ItemRemoved += RealEstateRepoOnItemRemoved;


        }
        void IUserCommunication.CommunicationWithUser()
        {

            bool runProgram = true;
            while (runProgram)
            {
                PrintMenu();
                string x = "0";
                x = Console.ReadLine();
                switch (x)
                {
                    case "1":
                        foreach (var item in _reRepository.GetAll())
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
                        if (y == "d")
                        {
                            Console.WriteLine("Wpisz nazwę domu");
                            string name = Console.ReadLine();
                            Console.WriteLine("Wpisz wartość domu");
                            int value = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Wpisz powierzchnię domu");
                            int area = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Wpisz powierzchnię działki domu");
                            int landArea = Int32.Parse(Console.ReadLine());
                            _reRepository.Add(new House(name, value, area, landArea));
                        }
                        if (y == "m")
                        {
                            Console.WriteLine("Wpisz nazwę mieszkania");
                            string name = Console.ReadLine();
                            Console.WriteLine("Wpisz wartość mieszkania");
                            int value = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Wpisz powierzchnię mieszkania");
                            int area = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Wpisz piętro mieszkania");
                            int floor = Int32.Parse(Console.ReadLine());
                            _reRepository.Add(new Apartment(name, value, area, floor));
                        }
                        break;
                    case "3":
                        Console.WriteLine("Wpisz id nieruchomości do usunięcia");
                        int id = Int32.Parse(Console.ReadLine());
                        var realEstate = _reRepository.GetById(id);
                        if (realEstate != null)
                        {
                            _reRepository.Remove(realEstate);
                        }
                        else
                        {
                            Console.WriteLine("Wrong ID");
                        }
                        break;
                    case "4":
                        _reRepository.Save();
                        runProgram = false;
                        break;
                    case "5":

                        Console.WriteLine(_reProvider.FirstByValue().ToString());
                        break;
                    case "6":

                        
                        int val;
                        while (true)
                        {
                            Console.WriteLine("Podaj ID");
                            var input = Console.ReadLine();
                            ;
                            if (int.TryParse(input, out val))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("złe id");
                            }
                        }

                        Console.WriteLine(_reProvider.SingleOrDefaultById(val).ToString());

                        break;
                    case "7":
                        
                            foreach(var item in _reProvider.OrderByValue())
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    default:
                        break;
                }
            }
        }
            void PrintMenu()
            {
                Console.WriteLine("Witaj w programie do zarządzania nieruchomościami");
                Console.WriteLine("Wpisz 1 aby wyświetlić posiadane nieruchomości");
                Console.WriteLine("Wpisz 2 aby dodać nieruchomość");
                Console.WriteLine("Wpisz 3 aby usunąć nieruchomość");
                Console.WriteLine("Wpisz 4 aby zamknąć program");
                Console.WriteLine("Wpisz 5 aby wyświetlić najtańszą nieruchomość");
                Console.WriteLine("Wpisz 6 aby wyświetlić nieruchomość o podanym Id");
                Console.WriteLine("Wpisz 7 aby wyświetlić najtańsze nieruchomości");

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


        

       
    }
}

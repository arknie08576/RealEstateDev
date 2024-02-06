using RealEstateDev.Entities;
using RealEstateDev.Repositories;
using TDev.DataProviders;

namespace RealEstateDev.Services
{
    public class UserCommunication : IUserCommunication
    {
        private readonly IRepository<Apartment> _reARepository;
        private readonly IRepository<House> _reHRepository;
        private readonly IRealEstateProvider<Apartment> _reAProvider;
        private readonly IRealEstateProvider<House> _reHProvider;
        public UserCommunication(IRepository<Apartment> reARepository, IRepository<House> reHRepository, IRealEstateProvider<Apartment> reAProvider, IRealEstateProvider<House> reHProvider)
        {
            _reARepository = reARepository;
            _reHRepository = reHRepository;
            _reAProvider = reAProvider;
            _reHProvider = reHProvider;
            _reARepository.ItemAdded += RealEstateRepoOnItemAdded;
            _reARepository.ItemRemoved += RealEstateRepoOnItemRemoved;
            _reHRepository.ItemAdded += RealEstateRepoOnItemAdded;
            _reHRepository.ItemRemoved += RealEstateRepoOnItemRemoved;


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
                        foreach (var item in _reARepository.GetAll())
                        {
                            Console.WriteLine(item);
                        }
                        foreach (var item in _reHRepository.GetAll())
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
                            _reHRepository.Add(new House(name, value, area, landArea));
                            _reHRepository.Save();
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
                            _reARepository.Add(new Apartment(name, value, area, floor));
                            _reARepository.Save();
                        }
                        break;
                    case "3":
                        string yy = "a";
                        while (!(yy == "d" || yy == "m"))
                        {
                            Console.WriteLine("Wpisz d jeśli chcesz usunąć dom lub m jeśli chcesz usunąć mieszkanie");
                            yy = Console.ReadLine();
                        }
                        if (yy == "d")
                        {
                            Console.WriteLine("Wpisz id domu do usunięcia");
                            int idd = Int32.Parse(Console.ReadLine());
                            var house = _reHRepository.GetById(idd);
                            if (house != null)
                            {
                                _reHRepository.Remove(house);
                            }
                            else
                            {
                                Console.WriteLine("Wrong ID");
                            }
                            _reHRepository.Save();
                        }
                        if (yy == "m")
                        {
                            Console.WriteLine("Wpisz id mieszkania do usunięcia");
                            int idd = Int32.Parse(Console.ReadLine());
                            var apartment = _reARepository.GetById(idd);
                            if (apartment != null)
                            {
                                _reARepository.Remove(apartment);
                            }
                            else
                            {
                                Console.WriteLine("Wrong ID");
                            }
                            _reARepository.Save();
                        }

                        break;
                    case "4":
                        _reARepository.Save();
                        _reHRepository.Save();
                        runProgram = false;
                        break;
                    case "5":

                        Console.WriteLine(_reHProvider.FirstByValue().ToString());
                        break;
                    case "6":

                        Console.WriteLine(_reAProvider.FirstByValue().ToString());
                        break;


                    case "7":


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

                        Console.WriteLine(_reHProvider.SingleOrDefaultById(val).ToString());

                        break;
                    case "8":


                        int valu;
                        while (true)
                        {
                            Console.WriteLine("Podaj ID");
                            var input = Console.ReadLine();
                            ;
                            if (int.TryParse(input, out valu))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("złe id");
                            }
                        }

                        Console.WriteLine(_reAProvider.SingleOrDefaultById(valu).ToString());

                        break;
                    case "9":

                        foreach (var item in _reHProvider.OrderByValue())
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case "10":

                        foreach (var item in _reAProvider.OrderByValue())
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case "11":
                        Console.WriteLine("Wpisz id domu do edycji");
                        int vala;
                        while (true)
                        {
                            vala = Int32.Parse(Console.ReadLine());
                            var house = _reHRepository.GetById(vala);
                            if (house != null)
                            {
                                _reHRepository.Remove(house);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong ID");
                            }
                        }

                        Console.WriteLine(_reHProvider.SingleOrDefaultById(vala).ToString());
                        Console.WriteLine("Wpisz nazwę domu");
                        string namee = Console.ReadLine();
                        Console.WriteLine("Wpisz wartość domu");
                        int valuee = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Wpisz powierzchnię domu");
                        int areaa = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Wpisz powierzchnię działki domu");
                        int landAreaa = Int32.Parse(Console.ReadLine());
                        var h = new House(namee, valuee, areaa, landAreaa);
                        h.Id = vala;

                        _reHRepository.Add(h);
                        _reHRepository.Save();
                        break;
                    case "12":
                        Console.WriteLine("Wpisz id mieszkania do edycji");
                        int valq;
                        while (true)
                        {
                            valq = Int32.Parse(Console.ReadLine());
                            var house = _reARepository.GetById(valq);
                            if (house != null)
                            {
                                _reARepository.Remove(house);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong ID");
                            }
                        }
                        Console.WriteLine("Wpisz nazwę mieszkania");
                        string nameq = Console.ReadLine();
                        Console.WriteLine("Wpisz wartość mieszkania");
                        int valueq = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Wpisz powierzchnię mieszkania");
                        int areaq = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Wpisz piętro mieszkania");
                        int floorq = Int32.Parse(Console.ReadLine());
                        var a = new Apartment(nameq, valueq, areaq, floorq);
                        a.Id = valq;
                        _reARepository.Add(a);
                        _reARepository.Save();
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
            Console.WriteLine("Wpisz 5 aby wyświetlić najtańszy dom");
            Console.WriteLine("Wpisz 6 aby wyświetlić najtańsze mieszkanie");
            Console.WriteLine("Wpisz 7 aby wyświetlić dom o podanym Id");
            Console.WriteLine("Wpisz 8 aby wyświetlić mieszkanie o podanym Id");
            Console.WriteLine("Wpisz 9 aby wyświetlić najtańsze domy");
            Console.WriteLine("Wpisz 10 aby wyświetlić najtańsze mieszkania");
            Console.WriteLine("Wpisz 11 aby edytować dom");
            Console.WriteLine("Wpisz 12 aby edytować mieszkanie");

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

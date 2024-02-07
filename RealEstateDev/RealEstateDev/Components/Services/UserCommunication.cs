using RealEstateDev.Entities;
using RealEstateDev.Repositories;
using TDev.DataProviders;

namespace RealEstateDev.Services
{
    public class UserCommunication : IUserCommunication
    {
        private readonly IRepository<Apartment> _apartmentsRepository;
        private readonly IRepository<House> _housesRepository;
        private readonly IRealEstateProvider<Apartment> _apartmentsProvider;
        private readonly IRealEstateProvider<House> _housesProvider;
        public UserCommunication(IRepository<Apartment> apartmentsRepository, IRepository<House> housesRepository, IRealEstateProvider<Apartment> reAProvider, IRealEstateProvider<House> reHProvider)
        {
            _apartmentsRepository = apartmentsRepository;
            _housesRepository = housesRepository;
            _apartmentsProvider = reAProvider;
            _housesProvider = reHProvider;
            _apartmentsRepository.ItemAdded += RealEstateRepoOnItemAdded;
            _apartmentsRepository.ItemRemoved += RealEstateRepoOnItemRemoved;
            _housesRepository.ItemAdded += RealEstateRepoOnItemAdded;
            _housesRepository.ItemRemoved += RealEstateRepoOnItemRemoved;
        }
        void IUserCommunication.CommunicationWithUser()
        {

            bool runProgram = true;
            while (runProgram)  
            {
                PrintMenu();
                string menuOption = "0";
                menuOption = Console.ReadLine();
                switch (menuOption)
                {
                    case "1":
                        foreach (var item in _apartmentsRepository.GetAll())
                        {
                            Console.WriteLine(item);
                        }
                        foreach (var item in _housesRepository.GetAll())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case "2":
                        string typeOfRealEstateToAdd = "a";
                        while (!(typeOfRealEstateToAdd == "d" || typeOfRealEstateToAdd == "m"))
                        {
                            Console.WriteLine("Wpisz d jeśli chcesz dodać dom lub m jeśli chcesz dodać mieszkanie");
                            typeOfRealEstateToAdd = Console.ReadLine();
                        }
                        if (typeOfRealEstateToAdd == "d")
                        {
                            Console.WriteLine("Wpisz nazwę domu");
                            string name = Console.ReadLine();
                            Console.WriteLine("Wpisz wartość domu");
                            int value = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Wpisz powierzchnię domu");
                            int area = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Wpisz powierzchnię działki domu");
                            int landArea = Int32.Parse(Console.ReadLine());
                            _housesRepository.Add(new House(name, value, area, landArea));
                            _housesRepository.Save();
                        }
                        if (typeOfRealEstateToAdd == "m")
                        {
                            Console.WriteLine("Wpisz nazwę mieszkania");
                            string name = Console.ReadLine();
                            Console.WriteLine("Wpisz wartość mieszkania");
                            int value = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Wpisz powierzchnię mieszkania");
                            int area = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Wpisz piętro mieszkania");
                            int floor = Int32.Parse(Console.ReadLine());
                            _apartmentsRepository.Add(new Apartment(name, value, area, floor));
                            _apartmentsRepository.Save();
                        }
                        break;
                    case "3":
                        string typeOfRealEstateToRemove = "a";
                        while (!(typeOfRealEstateToRemove == "d" || typeOfRealEstateToRemove == "m"))
                        {
                            Console.WriteLine("Wpisz d jeśli chcesz usunąć dom lub m jeśli chcesz usunąć mieszkanie");
                            typeOfRealEstateToRemove = Console.ReadLine();
                        }
                        if (typeOfRealEstateToRemove == "d")
                        {
                            Console.WriteLine("Wpisz id domu do usunięcia");
                            int houseRemoveId = Int32.Parse(Console.ReadLine());
                            var house = _housesRepository.GetById(houseRemoveId);
                            if (house != null)
                            {
                                _housesRepository.Remove(house);
                            }
                            else
                            {
                                Console.WriteLine("Wrong ID");
                            }
                            _housesRepository.Save();
                        }
                        if (typeOfRealEstateToRemove == "m")
                        {
                            Console.WriteLine("Wpisz id mieszkania do usunięcia");
                            int apartmentRemoveId = Int32.Parse(Console.ReadLine());
                            var apartment = _apartmentsRepository.GetById(apartmentRemoveId);
                            if (apartment != null)
                            {
                                _apartmentsRepository.Remove(apartment);
                            }
                            else
                            {
                                Console.WriteLine("Wrong ID");
                            }
                            _apartmentsRepository.Save();
                        }

                        break;
                    case "4":
                        _apartmentsRepository.Save();
                        _housesRepository.Save();
                        runProgram = false;
                        break;
                    case "5":

                        Console.WriteLine(_housesProvider.FirstByValue().ToString());
                        break;
                    case "6":

                        Console.WriteLine(_apartmentsProvider.FirstByValue().ToString());
                        break;


                    case "7":


                        int houseId;
                        while (true)
                        {
                            Console.WriteLine("Podaj ID");
                            var input = Console.ReadLine();
                            ;
                            if (int.TryParse(input, out houseId))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("złe id");
                            }
                        }

                        Console.WriteLine(_housesProvider.SingleOrDefaultById(houseId).ToString());

                        break;
                    case "8":


                        int apartmentId;
                        while (true)
                        {
                            Console.WriteLine("Podaj ID");
                            var input = Console.ReadLine();
                            ;
                            if (int.TryParse(input, out apartmentId))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("złe id");
                            }
                        }

                        Console.WriteLine(_apartmentsProvider.SingleOrDefaultById(apartmentId).ToString());

                        break;
                    case "9":

                        foreach (var item in _housesProvider.OrderByValue())
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case "10":

                        foreach (var item in _apartmentsProvider.OrderByValue())
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case "11":
                        Console.WriteLine("Wpisz id domu do edycji");
                        int editedHouseId;
                        while (true)
                        {
                            editedHouseId = Int32.Parse(Console.ReadLine());
                            var house = _housesRepository.GetById(editedHouseId);
                            if (house != null)
                            {
                                _housesRepository.Remove(house);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong ID");
                            }
                        }

                        Console.WriteLine(_housesProvider.SingleOrDefaultById(editedHouseId).ToString());
                        Console.WriteLine("Wpisz nazwę domu");
                        string editedHouseName = Console.ReadLine();
                        Console.WriteLine("Wpisz wartość domu");
                        int editedHouseValue = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Wpisz powierzchnię domu");
                        int editedHouseArea = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Wpisz powierzchnię działki domu");
                        int editedHouseLandArea = Int32.Parse(Console.ReadLine());
                        var editedHouse = new House(editedHouseName, editedHouseValue, editedHouseArea, editedHouseLandArea);
                        editedHouse.Id = editedHouseId;

                        _housesRepository.Add(editedHouse);
                        _housesRepository.Save();
                        break;
                    case "12":
                        Console.WriteLine("Wpisz id mieszkania do edycji");
                        int editedApartmentId;
                        while (true)
                        {
                            editedApartmentId = Int32.Parse(Console.ReadLine());
                            var house = _apartmentsRepository.GetById(editedApartmentId);
                            if (house != null)
                            {
                                _apartmentsRepository.Remove(house);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong ID");
                            }
                        }
                        Console.WriteLine("Wpisz nazwę mieszkania");
                        string editedApartmentName = Console.ReadLine();
                        Console.WriteLine("Wpisz wartość mieszkania");
                        int editedApartmentValue = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Wpisz powierzchnię mieszkania");
                        int editedApartmentArea = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Wpisz piętro mieszkania");
                        int editedApartmentFloor = Int32.Parse(Console.ReadLine());
                        var editedApartment = new Apartment(editedApartmentName, editedApartmentValue, editedApartmentArea, editedApartmentFloor);
                        editedApartment.Id = editedApartmentId;
                        _apartmentsRepository.Add(editedApartment);
                        _apartmentsRepository.Save();
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

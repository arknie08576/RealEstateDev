using RealEstateDev.Data;
using Microsoft.Extensions.DependencyInjection;
using RealEstateDev;
using RealEstateDev.Components.DataProviders;
using RealEstateDev.Repositories;
using RealEstateDev.Entities;
using RealEstateDev.Services;
using RealEstateDev.Components.CsvReader;
using RealEstateDev.Components.XmlCreator;
using Microsoft.EntityFrameworkCore;
using TDev.DataProviders;

//Data Source=DESKTOP-TEFRQV5\SQLEXPRESS;Initial Catalog=TestStorage;Integrated Security=True

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Apartment>, SqlRepository<Apartment>>();
services.AddSingleton<IRepository<House>, SqlRepository<House>>();
services.AddSingleton<IRealEstateProvider<Apartment>, RealEstateProvider<Apartment>>();
services.AddSingleton<IRealEstateProvider<House>, RealEstateProvider<House>>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IXmlCreator, XmlCreator>();
//services.AddDbContext<RealEstateDevDbContex>(options=>options.UseSqlServer("Data Source=DESKTOP-TEFRQV5\\SQLEXPRESS;Initial Catalog=TestStorage;Integrated Security=True"));
services.AddDbContext<RealEstateDevDbContex>(options => options.UseSqlServer("Data Source=DESKTOP-QSMS3V3\\PANJUTORIALSSQL;Initial Catalog=TestStorage;Integrated Security=True"));
var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();

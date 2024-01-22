using RealEstateDev.Data;
using System.Drawing;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;
using RealEstateDev;
using RealEstateDev.Components.DataProviders;
using RealEstateDev.Repositories;
using RealEstateDev.Entities;
using RealEstateDev.DataProviders;
using RealEstateDev.Services;
using RealEstateDev.Components.CsvReader;
using RealEstateDev.Components.XmlCreator;
using Microsoft.EntityFrameworkCore;

//Data Source=DESKTOP-TEFRQV5\SQLEXPRESS;Initial Catalog=TestStorage;Integrated Security=True

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<RealEstate>, FileRepository<RealEstate>>();
services.AddSingleton<IRealEstateProvider, RealEstateProvider>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IXmlCreator, XmlCreator>();
services.AddDbContext<RealEstateDevDbContex>(options=>options.UseSqlServer("Data Source=DESKTOP-TEFRQV5\\SQLEXPRESS;Initial Catalog=TestStorage;Integrated Security=True"));
var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();

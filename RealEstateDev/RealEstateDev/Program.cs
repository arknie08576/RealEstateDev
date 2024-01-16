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

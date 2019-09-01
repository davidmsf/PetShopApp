using Microsoft.Extensions.DependencyInjection;
using PetShopApp.Core.ApplicationService;
using PetShopApp.Core.ApplicationService.Services;
using PetShopApp.Core.DomainService;
using PetShopApp.Infrastructure.Static.Data.Repositories;
using System;

namespace PetConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IPetRepository, PetRepository>();
            serviceCollection.AddScoped<IPetService, PetService>();
            serviceCollection.AddScoped<IConsolePrinter, ConsolePrinter>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var consolePrinter = serviceProvider.GetRequiredService<IConsolePrinter>();
            consolePrinter.StartUI();

        }
    }
}

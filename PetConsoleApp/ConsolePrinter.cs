using PetShopApp.Core.DomainService;
using PetShopApp.Infrastructure.Static.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetConsoleApp
{
    public class ConsolePrinter
    {
        static IPetRepository petRepository;
        string[] menuItems = {
                "Show all pets",
                "Add new pet",
                "Edit pet",
                "Delete pet",
                "Find pet",
                "Exit"
        };

        public ConsolePrinter()
        {
            petRepository = new PetRepository();
            var selection = ShowMenu();
            HandleSelection(selection);

        }

        int ShowMenu()
        {


            Console.WriteLine("Select an action from the menu by selecting the menuitem number:");

            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine($"{(i + 1)}. {menuItems[i]}");
            }

            int number;
            string value = Console.ReadLine();

            while (!int.TryParse(value, out number)
                || number < 1
                || number > menuItems.Length)
            {
                Console.WriteLine("Has to be a number");
                value = Console.ReadLine();
            }

            return number;
        }


        void HandleSelection(int selection)
        {
            while (selection != menuItems.Length)
            {
                Console.Clear();
                switch (selection)
                {
                    case 1:
                        //ShowAllVideos();
                        break;

                    case 2:
                        
                        //AddVideo(Console.ReadLine());
                        break;

                    case 3:
                        //EditVideo(GetIdFromUser());
                    
                        break;

                    case 4:
                        //DeleteVideo(GetIdFromUser());
                       
                        break;

                }

                selection = ShowMenu();

            }
        }
    }
}

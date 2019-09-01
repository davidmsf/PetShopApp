using PetShopApp.Core.ApplicationService;
using PetShopApp.Core.ApplicationService.Services;
using PetShopApp.Core.DomainService;
using PetShopApp.Core.Entity;
using PetShopApp.Infrastructure.Static.Data;
using PetShopApp.Infrastructure.Static.Data.Repositories;
using System;
using System.Collections.Generic;

namespace PetConsoleApp
{
    public class ConsolePrinter
    {
        private readonly IPetService _petService;
        static IPetRepository petRepository;

        string[] menuItems = 
        {
                "Show all pets",
                "Search by type",
                "Add",
                "Delete",
                "Update",
                "Sort pets by price",
                "Show 5 cheapest available Pets",
                "Exit"
        };

        public ConsolePrinter()
        {
            
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();

            FakeDB.InitData();

            petRepository = new PetRepository();
            _petService = new PetService(petRepository);

            var selection = ShowMenu();
            HandleSelection(selection);

        }

        int ShowMenu()
        {


            Console.WriteLine("\nSelect an action from the menu by selecting the menuitem number: \n");

            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine($"{(i + 1)}. {menuItems[i]}");
            }

            int number;
            while (!int.TryParse(Console.ReadLine(), out number)
                || number < 1
                || number > menuItems.Length)
            {
                Console.WriteLine($"Has to be a number between 1 and {menuItems.Length}");


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
                        ShowAllPets(_petService.ReadPets());
                        break;

                    case 2:
                        SearchByType();
                        break;

                    case 3:
                        CreatePet();
                        break;

                    case 4:
                        Delete(GetIdFromUser());
                        break;

                    case 5:
                        UpdatePet(_petService.FindPetById(GetIdFromUser()));
                        break;

                    case 6:
                        SortByPrice();
                        break;

                    case 7:
                        FiveCheapestPets();
                        break;
                }
                selection = ShowMenu();
            }
        }

        private void SearchByType()
        {
            List<Pet> petsByType = _petService.SearchByType(PrintQuestion("Write the type of the pet:"));
            ShowAllPets(petsByType);
        }

        private void FiveCheapestPets()
        {
            ShowAllPets(_petService.GetFiveCheapestPets());
        }

        private void SortByPrice()
        {
            ShowAllPets(_petService.OrderByPrice());
        }

        private void CreatePet()
        {
            string name, color, previousOwner, type;
            double price;
            DateTime soldDate, birthDate;

            insertPetProperties(out name,
                                out color,
                                out previousOwner,
                                out type,
                                out price,
                                out soldDate,
                                out birthDate);

            Pet pet = _petService.NewPet(name,
                               color,
                               previousOwner,
                               type,
                               price,
                               soldDate,
                               birthDate);

            var SavedPet = _petService.CreatePet(pet);

            if (SavedPet.Id > 0)
            {
                Console.WriteLine("The pet has been added");
            }
        }

        private void insertPetProperties(out string name,
                                            out string color, 
                                            out string previousOwner, 
                                            out string type, 
                                            out double price, 
                                            out DateTime soldDate, 
                                            out DateTime birthDate)
        {
            name = PrintQuestion("Write the name of the pet:");
            color = PrintQuestion("Write the color of the pet:");
            previousOwner = PrintQuestion("Write the name of the previous owner:");
            type = PrintQuestion("Write the type of the pet:");
            price = PrintQuestionReturnDouble("Write the price of the pet:");
            soldDate = PrintQuestionReturnDateTime("Write the sold date of the pet(dd/mm/yyyy):");
            birthDate = PrintQuestionReturnDateTime("Write the birth date of the pet(dd/mm/yyyy):");
        }

        private void Delete(int id)
        {
            _petService.Delete(id);
        }

        void UpdatePet(Pet pet)
        {
          
            Console.WriteLine("1. Name - " + pet.Name);
            Console.WriteLine("2. Type - " + pet.Type);
            Console.WriteLine("3. Previous owner - " + pet.PreviousOwner);
            Console.WriteLine("4. Price - " + pet.Price);
            Console.WriteLine("5. Sold date - " + pet.SoldDate);
            Console.WriteLine("6. Bith date - " + pet.BirthDate);
            Console.WriteLine("7. Color - " + pet.Color);

            int selection;
            Console.WriteLine("Select the specific pet info you wish to edit by entering the associated number:");
            while (!int.TryParse(Console.ReadLine(), out selection)
                || selection > 7)
            {
                Console.WriteLine("Select a number:");
            }
            
            
            switch (selection)
            {
                case 1:
                    pet.Name = PrintQuestion("Write the name of the pet:");
                    break;

                case 2:
                    pet.Type = PrintQuestion("Write the type of the pet:");
                    break;

                case 3:
                    pet.PreviousOwner = PrintQuestion("Write the previous owner of the pet:");                    
                    break;

                case 4:
                    pet.Price = PrintQuestionReturnDouble("Write the price of the pet:");                    
                    break;

                case 5:
                    pet.SoldDate = PrintQuestionReturnDateTime("Write the sold date of the pet(dd/mm/yyyy):");                    
                    break;

                case 6:
                    pet.BirthDate = PrintQuestionReturnDateTime("Write the birth date of the pet(dd/mm/yyyy):");                    
                    break;

                case 7: 
                    pet.Color = PrintQuestion("Write the color of the pet:");                    
                    break;
            }

            _petService.UpdatePet(pet);
        }

        void ShowAllPets(List<Pet> pets)
        {
            
            foreach (var pet in pets)
            {
                Console.WriteLine($"Id: {pet.Id}");
                Console.WriteLine($"Name: {pet.Name}");
                Console.WriteLine($"Type: {pet.Type}");
                Console.WriteLine($"Price: {pet.Price}");
                Console.WriteLine($"Color: {pet.Color}");
                Console.WriteLine($"Birthdate: {pet.BirthDate.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"Previous owner: {pet.PreviousOwner}");
                Console.WriteLine($"Sold date: {pet.SoldDate.ToString("dd/MM/yyyy")}\n");               
            }
        }

        int GetIdFromUser()
        {
            Console.WriteLine("Enter pet id");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("write a number");
            }
            return id;
        }

        string PrintQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        double PrintQuestionReturnDouble(string question)
        {
            double @double;
            Console.WriteLine(question);
            while (!double.TryParse(Console.ReadLine(), out @double))
            {
                Console.WriteLine(question);
            }
            return @double;
        }

        DateTime PrintQuestionReturnDateTime(string question)
        {
            Console.WriteLine(question);
            DateTime dateTime;
            while (!DateTime.TryParseExact(Console.ReadLine(),
                "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dateTime))
            {
                Console.WriteLine(question);
            }

            return dateTime;
        }
        
    }
}

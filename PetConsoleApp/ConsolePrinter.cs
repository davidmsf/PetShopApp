using PetShopApp.Core.DomainService;
using PetShopApp.Core.Entity;
using PetShopApp.Infrastructure.Static.Data;
using PetShopApp.Infrastructure.Static.Data.Repositories;
using System;


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
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            FakeDB.InitData();
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
                        ShowAllPets();
                        break;

                    case 2:
                        var name = PrintQuestion("Write the name of the pet:");

                        var color = PrintQuestion("Write the color of the pet:");

                        var previousOwner = PrintQuestion("Write the name of the previous owner:");

                        var type = PrintQuestion("Write the type of the pet:");

                        var price = PrintQuestionReturnDouble("Write the price of the pet:");

                        var soldDate = PrintQuestionReturnDateTime("Write the sold date of the pet(dd/mm/yyyy):");

                        var birthDate = PrintQuestionReturnDateTime("Write the birth date of the pet(dd/mm/yyyy):");

                        var pet = CreatePet(name,
                                    color,
                                    previousOwner,
                                    type,
                                    price,
                                    soldDate,
                                    birthDate);

                        var SavedPet = SavePet(pet);
                        if (SavedPet.Id > 0)
                        {
                            Console.WriteLine("The pet has been added");
                        }

                        break;

                    case 3:
                        UpdatePet(petRepository.GetPetById(GetIdFromUser()));
                        break;

                    case 4:
                        //DeleteVideo(GetIdFromUser());

                        break;

                }

                selection = ShowMenu();

            }
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
            while (!int.TryParse(Console.ReadLine(), out selection))
            {
                Console.WriteLine("Select a number:");
            }
            
            string property = "";
            switch (selection)
            {
                case 1:

                    pet.Name = PrintQuestion("Write the name of the pet:");
                    property = "Name";
                    break;

                case 2:

                    pet.Type = PrintQuestion("Write the type of the pet:");
                    property = "Type";
                    break;

                case 3:

                    pet.PreviousOwner = PrintQuestion("Write the previous owner of the pet:");
                    property = "PreviousOwner";
                    break;

                case 4:

                    pet.Price = PrintQuestionReturnDouble("Write the price of the pet:");
                    property = "Price";
                    break;

                case 5:

                    pet.SoldDate = PrintQuestionReturnDateTime("Write the sold date of the pet(dd/mm/yyyy):");
                    property = "SoldDate";
                    break;

                case 6:

                    pet.BirthDate = PrintQuestionReturnDateTime("Write the birth date of the pet(dd/mm/yyyy):");
                    property = "BirthDate";
                    break;

                case 7:
 
                    pet.Color = PrintQuestion("Write the color of the pet:");
                    property = "Color";

                    break;
            }
            

            //var propertyType = pet.GetType().GetProperty(property);
            //propertyType.SetValue(pet, pet.GetType().GetProperty(property).GetValue(pet));
            petRepository.UpdatePet(pet, property);
        }

        Pet CreatePet(string name,
                        string color, 
                        string previousOwner, 
                        string type, 
                        double price, 
                        DateTime soldDate, 
                        DateTime birthDate)
        {
            Pet pet = new Pet
            {
                Name = name,

                Color = color,

                PreviousOwner = previousOwner,

                Type = type,

                Price = price,

                SoldDate = soldDate,

                BirthDate = birthDate
            };

            return pet;
            
        }
        
        Pet SavePet(Pet pet)
        {
            return petRepository.CreatePet(pet); 
        }

        void ShowAllPets()
        {
            
            foreach (var pet in petRepository.ReadPets())
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

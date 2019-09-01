using PetShopApp.Core.ApplicationService;
using PetShopApp.Core.Entity;
using PetShopApp.Infrastructure.Static.Data;
using System;
using System.Collections.Generic;

namespace PetConsoleApp
{
    public class ConsolePrinter : IConsolePrinter
    {
        private IPetService _petService;

        //array of menuitems for the primary menu
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

        public ConsolePrinter(IPetService petService)
        {
            _petService = petService;

        }
          
        /// <summary>
        /// Creates the static data from the fake database(pet objects), 
        /// calls the method for showing the primary menu, and another method for the selection of the primary menu 
        /// </summary>
        public void StartUI()
        {

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();

            FakeDB.InitData();

            var selection = ShowMenu();
            HandleSelection(selection);
        }

        /// <summary>
        /// Prints the menuitems and then waits for the user input using a while loop
        /// </summary>
        /// <returns></returns>
        public int ShowMenu()
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
        

        /// <summary>
        /// Using a switch statement to call the method assóciated with the users selection
        /// </summary>
        /// <param name="selection"></param>
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


        /// <summary>
        /// Calls the petservice find the type of pet the user has entered, then calls show all pets to show the searchresult 
        /// </summary>
        private void SearchByType()
        {
            List<Pet> petsByType = _petService.SearchByType(PrintQuestion("Write the type of the pet:"));
            ShowAllPets(petsByType);
        }

        /// <summary>
        /// Calls the petservice find the five cheapest pets, then calls show all pets to show the searchresult 
        /// </summary>
        private void FiveCheapestPets()
        {
            ShowAllPets(_petService.GetFiveCheapestPets());
        }

        /// <summary>
        /// Calls the petservice find order the pets by price, then calls show all pets to show the searchresult 
        /// </summary>
        private void SortByPrice()
        {
            ShowAllPets(_petService.OrderByPrice());
        }

        /// <summary>
        /// Calls the insertPetProperties method for the user to input the pet data, which returns the pets properties.
        /// Calls the petservice to create a new pet object, 
        /// then calls save pet which sends the newly created pet, and returns the saved  
        /// </summary>
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

            var SavedPet = _petService.SavePet(pet);

            if (SavedPet.Id > 0)
            {
                Console.WriteLine("The pet has been added");
            }
        }

        /// <summary>
        /// asks the user for the properties when creating a new pet
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="previousOwner"></param>
        /// <param name="type"></param>
        /// <param name="price"></param>
        /// <param name="soldDate"></param>
        /// <param name="birthDate"></param>
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

        /// <summary>
        /// Calls to the petservice to delete a specific pet by id
        /// </summary>
        /// <param name="id"></param>
        private void Delete(int id)
        {
            _petService.Delete(id);
        }

        /// <summary>
        /// The user can choose which parameter to update. 
        /// When the user has written the new parametervalue the pet ís sent to petsercice
        /// </summary>
        /// <param name="pet"></param>
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

        /// <summary>
        /// Method used to loop through all pets from a list and print them
        /// </summary>
        /// <param name="pets"></param>
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

        /// <summary>
        /// Asks the user for a specific number used as id for the pet
        /// </summary>
        /// <returns>id</returns>
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

        /// <summary>
        /// Prints a question to the user and then asks for input which is returned as a string
        /// </summary>
        /// <param name="question"></param>
        /// <returns>user input as string</returns>
        string PrintQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        /// <summary>
        /// Prints a question to the user and then asks for input which is returned as a double
        /// </summary>
        /// <param name="question"></param>
        /// <returns>user input as double</returns>
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

        /// <summary>
        /// Prints a question to the user and then asks for input which is returned as a DateType
        /// </summary>
        /// <param name="question"></param>
        /// <returns>user input as DateTime</returns>
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

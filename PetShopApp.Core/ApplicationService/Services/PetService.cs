using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetShopApp.Core.DomainService;
using PetShopApp.Core.Entity;

namespace PetShopApp.Core.ApplicationService.Services
{
    public class PetService : IPetService
    {
        readonly IPetRepository _petRepo;

        public PetService(IPetRepository petRepository)
        {
            _petRepo = petRepository;
        }

        public Pet CreatePet(Pet pet)
        {
            return _petRepo.CreatePet(pet);
        }


        public List<Pet> ReadPets()
        {
            return _petRepo.ReadPets().ToList();
        }


        public Pet FindPetById(int id)
        {
            return _petRepo.GetPetById(id);
        }

    
        public Pet UpdatePet(Pet updatePet)
        {
            return _petRepo.UpdatePet(updatePet);
        }
        

        public void Delete(int id)
        {
            _petRepo.Delete(id);
        }


        public Pet NewPet(string name, 
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

        public List<Pet> SearchByType(string type)
        {
            return _petRepo.ReadPets().Where(pet => pet.Type.Equals(type, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

       public List<Pet> OrderByPrice()
        {
            return _petRepo.ReadPets().OrderBy(pet => pet.Price).ToList();
        }

        public List<Pet> GetFiveCheapestPets()
        {
            return _petRepo.ReadPets().OrderBy(pet => pet.Price).Take(5).ToList();
        }
    }
}

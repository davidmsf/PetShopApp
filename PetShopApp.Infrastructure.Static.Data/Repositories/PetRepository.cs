using PetShopApp.Core.DomainService;
using PetShopApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetShopApp.Infrastructure.Static.Data.Repositories
{
    public class PetRepository : IPetRepository
    {
     

        public Pet CreatePet(Pet pet)
        {
            pet.Id = FakeDB.id++;

            FakeDB.pets.Add(pet);
  
            return pet;
        }

        public void Delete(int id)
        {
           
            var petToDelete = FakeDB.pets.FirstOrDefault(pet => pet.Id == id);
            FakeDB.pets.Remove(petToDelete);
           
        }

        public Pet GetPetById(int id)
        {
            return FakeDB.pets.FirstOrDefault(pet => pet.Id == id);
        }

        public IEnumerable<Pet> ReadPets()
        {
            return FakeDB.pets;
        }

        public Pet UpdatePet(Pet updatedPet)
        {
            var pet = this.GetPetById(updatedPet.Id);
            if (pet != null)
            {
                pet.Name = updatedPet.Name;
                pet.PreviousOwner = updatedPet.PreviousOwner;
                pet.Price = updatedPet.Price;
                pet.SoldDate = updatedPet.SoldDate;
                pet.Type = updatedPet.Type;
                pet.BirthDate = updatedPet.BirthDate;
                pet.Color = updatedPet.Color;
                return pet;
            }
            return null;
        }
    }
}

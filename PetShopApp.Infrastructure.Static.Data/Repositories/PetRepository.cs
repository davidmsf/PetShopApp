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
            var pets = FakeDB.pets.ToList();
            pets.Add(pet);
            FakeDB.pets = pets;
            return pet;
        }

        public void Delete(int id)
        {
            var pets = FakeDB.pets.ToList();
            var petToDelete = FakeDB.pets.FirstOrDefault(pet => pet.Id == id);
            pets.Remove(petToDelete);
            FakeDB.pets = pets;
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

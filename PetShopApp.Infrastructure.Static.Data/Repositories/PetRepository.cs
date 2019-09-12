using PetShopApp.Core.DomainService;
using PetShopApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PetShopApp.Infrastructure.Static.Data.Repositories
{
    public class PetRepository : IPetRepository
    {

        /// <summary>
        /// Add a new pet to the list, and assign the list with the new pet to the IEnumerable<Pet> in fakeDB
        /// </summary>
        /// <param name="pet"></param>
        /// <returns>The newly created pet</returns>
        public Pet CreatePet(Pet pet)
        {
            pet.Id = FakeDB.id++;
            var pets = FakeDB.pets.ToList();
            pets.Add(pet);
            FakeDB.pets = pets;
            return pet;
        }

        /// <summary>
        /// Delete a pet by specific id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
                var pets = FakeDB.pets.ToList();
                var petToDelete = pets.FirstOrDefault(pet => pet.Id == id);
                pets.Remove(petToDelete);
                FakeDB.pets = pets;
        }

        /// <summary>
        /// Get a specific pet by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>specified Pet</returns>
        public Pet GetPetById(int id)
        {
            return FakeDB.pets.FirstOrDefault(pet => pet.Id == id);
        }

        /// <summary>
        ///  return all pets 
        /// </summary>
        /// <returns>All pets</returns>
        public IEnumerable<Pet> ReadPets()
        {
            return FakeDB.pets;
        }

        /// <summary>
        /// First checks if the pet is stored and then updates its properties
        /// </summary>
        /// <param name="updatedPet"></param>
        /// <returns>The updated pet</returns>
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

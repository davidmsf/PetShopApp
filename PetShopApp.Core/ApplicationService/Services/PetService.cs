using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PetShopApp.Core.DomainService;
using PetShopApp.Core.Entity;
using PetShopApp.Core.Exceptions;

namespace PetShopApp.Core.ApplicationService.Services
{
    public class PetService : IPetService
    {
        readonly IPetRepository _petRepo;
        readonly IExceptionCreator _exceptionCreator;
        
        public PetService(IPetRepository petRepository, IExceptionCreator exceptionCreator)
        {
            _petRepo = petRepository;
            _exceptionCreator = exceptionCreator;
        }

        /// <summary>
        /// Gets all pets 
        /// </summary>
        /// <returns>List of pets</returns>
        public List<Pet> ReadPets()
        {
            return _petRepo.ReadPets().ToList();
        }

        /// <summary>
        /// Find a specific pet by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The found pet by id</returns>
        public Pet FindPetById(int id)
        {
            return _petRepo.GetPetById(id);
        }


        /// <summary>
        /// Call to save the new pet object in the repository and returns it aswell
        /// </summary>
        /// <param name="pet"></param>
        /// <returns>The newly saved pet</returns>
        public Pet SavePet(Pet pet)
        {
            if (string.IsNullOrEmpty(pet.Name))
            {
                _exceptionCreator.Invalid("You need to input the pets name");
            }
            
            if (pet.Id != 0)
            {
                _exceptionCreator.Invalid("You should not input an id for a pet, the db will handle it");
            }
               
            return _petRepo.CreatePet(pet);
        }


        /// <summary>
        /// Update a specific pet
        /// </summary>
        /// <param name="updatePet"></param>
        /// <returns>The updated pet</returns>
        public Pet UpdatePet(Pet updatePet)
        {
            var updatedPet = _petRepo.UpdatePet(updatePet);
            if (updatedPet == null)
            {
                _exceptionCreator.Invalid("Cannot Update the pet, could not find it");
            }
            return updatedPet;
        }

        /// <summary>
        /// Delete a specific pet by id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            if (this.FindPetById(id) == null)
            {
                _exceptionCreator.Invalid("Cannot delete the pet, could not find it");
            }
            
            _petRepo.Delete(id);
        }

        /// <summary>
        /// Creates a new pet object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="previousOwner"></param>
        /// <param name="type"></param>
        /// <param name="price"></param>
        /// <param name="soldDate"></param>
        /// <param name="birthDate"></param>
        /// <returns>The newly created pet</returns>
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

        /// <summary>
        /// Seaches all pets by a specific animal type from the type property, and returns the matches
        /// </summary>
        /// <param name="type"></param>
        /// <returns>List of pets of a specific type</returns>
        public List<Pet> SearchByType(string type)
        {
            return _petRepo.ReadPets().Where(pet => pet.Type.Equals(type, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        /// <summary>
        /// Return all pets ordered by price
        /// </summary>
        /// <returns>List of pets</returns>
        public List<Pet> OrderByPrice()
        {
        return _petRepo.ReadPets().OrderBy(pet => pet.Price).ToList();
        }

        /// <summary>
        /// Returns the five cheapest pets
        /// </summary>
        /// <returns>list of 5 cheapest pets</returns>
        public List<Pet> GetFiveCheapestPets()
        {
            return _petRepo.ReadPets().OrderBy(pet => pet.Price).Take(5).ToList();
        }
    }
}

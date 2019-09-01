using PetShopApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApp.Core.ApplicationService
{
    public interface IPetService
    {
        /// <summary>
        /// Update a specific pet
        /// </summary>
        /// <param name="updatePet"></param>
        /// <returns>The updated pet</returns>
        Pet UpdatePet(Pet updatePet);
            
        /// <summary>
        /// Find a specific pet by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The found pet by id</returns>
        Pet FindPetById(int id);

        /// <summary>
        /// Gets all pets 
        /// </summary>
        /// <returns>List of pets</returns>
        List<Pet> ReadPets();

        /// <summary>
        /// Delete a specific pet by id
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
        
        /// <summary>
        /// save the new pet object in the repository
        /// </summary>
        /// <param name="pet"></param>
        /// <returns>The newly saved pet</returns>
        Pet SavePet(Pet pet);

        /// <summary>
        /// create a new pet object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="previousOwner"></param>
        /// <param name="type"></param>
        /// <param name="price"></param>
        /// <param name="soldDate"></param>
        /// <param name="birthDate"></param>
        /// <returns>The newly created pet</returns>
        Pet NewPet(string name,
                        string color,
                        string previousOwner,
                        string type,
                        double price,
                        DateTime soldDate,
                        DateTime birthDate);

        /// <summary>
        /// Seach all pets for a specific type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>A list of the pets with the matching type</returns>
        List<Pet> SearchByType(string type);

        /// <summary>
        /// Order all pets by price
        /// </summary>
        /// <returns>List of pets ordered by price</returns>
        List<Pet> OrderByPrice();

        /// <summary>
        /// Get the five cheapest pets available
        /// </summary>
        /// <returns>List of the five cheapest pets</returns>
        List<Pet> GetFiveCheapestPets();
    }
}

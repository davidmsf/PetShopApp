using PetShopApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApp.Core.DomainService
{
    public interface IPetRepository
    {
        /// <summary>
        /// Get all pets
        /// </summary>
        /// <returns>List of pets</returns>
        List<Pet> ReadPets();

        /// <summary>
        /// Delete specific pet by id 
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Get specific pet by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Pet, specified by id</returns>
        Pet GetPetById(int id);

        /// <summary>
        /// Create a new pet
        /// </summary>
        /// <param name="pet"></param>
        /// <returns>The newly created pet</returns>
        Pet CreatePet(Pet pet);

        /// <summary>
        /// Update a specific pet
        /// </summary>
        /// <param name="pet"></param>
        /// <returns>The updated pet</returns>
        Pet UpdatePet(Pet pet);
    }
}

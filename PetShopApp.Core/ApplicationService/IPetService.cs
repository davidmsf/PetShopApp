using PetShopApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApp.Core.ApplicationService
{
    public interface IPetService
    {
        Pet UpdatePet(Pet updatePet);
            
        Pet FindPetById(int id);

        List<Pet> ReadPets();

        void Delete(int id);
        
        Pet CreatePet(Pet pet);

        Pet NewPet(string name,
                        string color,
                        string previousOwner,
                        string type,
                        double price,
                        DateTime soldDate,
                        DateTime birthDate);

        List<Pet> SearchByType(string type);
    }
}

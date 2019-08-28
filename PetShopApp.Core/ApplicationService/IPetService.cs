using PetShopApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApp.Core.ApplicationService
{
    public interface IPetService
    {
        Pet UpdatePet(Pet pet, string property);
        
    }
}

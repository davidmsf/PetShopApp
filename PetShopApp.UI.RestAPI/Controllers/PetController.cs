using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShopApp.Core.ApplicationService;
using PetShopApp.Core.Entity;

namespace PetShopApp.UI.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }


        // GET api/pets
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get()
        {
            return _petService.ReadPets();
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Pet> Get(int id)
        {
            return _petService.FindPetById(id);
        }


        // POST api/pet
        [HttpPost]
        public Pet Post([FromBody] Pet pet)
        {
            return _petService.SavePet(pet);
        }

        // DELETE api/pet/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _petService.Delete(id);
        }

        // PUT api/pet
        [HttpPut]
        public Pet Put(Pet pet)
        {
            return _petService.UpdatePet(pet);
        }
    }
}
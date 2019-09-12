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
        public ActionResult<Pet> Post([FromBody] Pet pet)
        {
            try
            {
                return Ok(_petService.SavePet(pet));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        // DELETE api/pet/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                _petService.Delete(id);
                return Ok("pet was deleted");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // PUT api/pet
        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, Pet pet)
        {
            if (id != pet.Id)
            {
                return BadRequest("Wrong pet id, could not update it");
            }
            
            try
            {
                return _petService.UpdatePet(pet);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
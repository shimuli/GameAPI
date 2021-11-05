using GameAPI.Model;
using GameAPI.Repository.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterRepo _characterRepo;
        public CharacterController(ICharacterRepo characterRepo)
        {
            _characterRepo = characterRepo;
        }


        [HttpGet]
        public  ActionResult<List<Character>> GetALl()
        {
            return Ok(_characterRepo.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public  ActionResult<Character> GetOne(int id)
        {
            var charcter = _characterRepo.GetOneCharacter(id);
            if(charcter == null)
            {
                return NotFound("Charcter not found");
            }
            return Ok(charcter);
        }

        [HttpPost]
        public ActionResult<List<Character>> AddCharacter([FromForm] Character newcharacter)
        {
            return Ok(_characterRepo.AddCharacter(newcharacter));
        }
    }
}

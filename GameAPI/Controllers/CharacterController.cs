using GameAPI.DTO.Characters;
using GameAPI.Helpers;
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
        public  async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>>  GetALl()
        {
            return Ok(await _characterRepo.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetOne(int id)
        {
            var charcter = _characterRepo.GetOneCharacter(id);
            if(charcter == null)
            {
                return NotFound(charcter);
            }
            return Ok( await charcter);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter([FromForm] CreateCharactorDto newcharacter)
        {
            return Ok(await _characterRepo.AddCharacter(newcharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter([FromForm] UpdateCharactorDto updateDto)
        {
            var charactor = await _characterRepo.UpdateCharactor(updateDto);
            if(charactor.Data == null)
            {
                return NotFound(charactor);
            }
            return Ok(charactor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Delete(int id)
        {
            var charactor = await _characterRepo.Delete(id);
            if (charactor.Data == null)
            {
                return NotFound(charactor);
            }
            return Ok(charactor);
        }
    }
}

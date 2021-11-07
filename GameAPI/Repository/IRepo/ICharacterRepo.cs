using GameAPI.DTO.Characters;
using GameAPI.Helpers;
using GameAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Repository.IRepo
{
    public interface ICharacterRepo
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterDto>> GetOneCharacter(int id);

        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(CreateCharactorDto newCharacter);

        Task<ServiceResponse<GetCharacterDto>> UpdateCharactor(UpdateCharactorDto updateCharactorDto);


        Task<ServiceResponse<List<GetCharacterDto>>> Delete(int Id);
    }
}

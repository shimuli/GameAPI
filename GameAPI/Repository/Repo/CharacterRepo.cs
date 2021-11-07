using AutoMapper;
using GameAPI.DTO.Characters;
using GameAPI.Helpers;
using GameAPI.Model;
using GameAPI.Repository.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Repository.Repo
{
    public class CharacterRepo : ICharacterRepo
    {

        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character{Id= 1,Name = "Jojo"}
        };
        private readonly IMapper _mapper;

        public CharacterRepo(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(CreateCharactorDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var newchar = _mapper.Map<Character>(newCharacter);
            newchar.Id = characters.Max(c => c.Id) + 1;
            characters.Add(newchar);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }



        public async Task<ServiceResponse <List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>>  GetOneCharacter(int id)
        {
            var charcter = characters.FirstOrDefault(c => c.Id == id);
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(charcter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharactor(UpdateCharactorDto updateCharactorDto)
        {
            var serviceRespones = new ServiceResponse<GetCharacterDto>();
          
            try
            {
                Character updateCharacter = characters.FirstOrDefault(c => c.Id == updateCharactorDto.Id);
                if(updateCharacter != null)
                {
                    updateCharacter.Name = updateCharactorDto.Name;
                    updateCharacter.HitPoint = updateCharactorDto.HitPoint;
                    updateCharacter.Intelligence = updateCharactorDto.Intelligence;
                    updateCharacter.Strength = updateCharactorDto.Strength;
                    updateCharacter.Defence = updateCharactorDto.Defence;
                    updateCharacter.Class = updateCharactorDto.Class;
                    serviceRespones.Data = _mapper.Map<GetCharacterDto>(updateCharacter);
                }
                else
                {
                    serviceRespones.Success = false;
                    serviceRespones.Message = "Item not found";
                }
                
            }
            catch(Exception ex)
            {
                serviceRespones.Success = false;
                serviceRespones.Message = ex.Message;
            }
            
            return serviceRespones;
        }


        public async Task<ServiceResponse<List<GetCharacterDto>>> Delete(int id)
        {
            var serviceRespones = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                Character removeCharacter = characters.First(c=>c.Id == id);
                if (removeCharacter != null)
                {
                    characters.Remove(removeCharacter);
                    serviceRespones.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                }
                else
                {
                    serviceRespones.Success = false;
                    serviceRespones.Message = "Item not found";
                }
               

            }
            catch (Exception ex)
            {
                serviceRespones.Success = false;
                serviceRespones.Message = ex.Message;
            }

            return serviceRespones;
        }
    }
}

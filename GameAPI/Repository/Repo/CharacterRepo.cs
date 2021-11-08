using AutoMapper;
using GameAPI.Data;
using GameAPI.DTO.Characters;
using GameAPI.Helpers;
using GameAPI.Model;
using GameAPI.Repository.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Repository.Repo
{
    public class CharacterRepo : ICharacterRepo
    {

        //private static List<Character> characters = new List<Character>
        //{
        //    new Character(),
        //    new Character{Id= 1,Name = "Jojo"}
        //};
        private readonly IMapper _mapper;
        private readonly DataContext _db;

        public CharacterRepo(IMapper mapper, DataContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(CreateCharactorDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var newchar = _mapper.Map<Character>(newCharacter);
            newchar.DateCreated = DateTime.Now;
            newchar.DateUpdated = DateTime.Now;
            await _db.Characters.AddAsync(newchar);
            await _db.SaveChangesAsync();
            serviceResponse.Data = _db.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }



        public async Task<ServiceResponse <List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>
            {
                Data = await _db.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>>  GetOneCharacter(int id)
        {
            var charcter =await _db.Characters.FirstOrDefaultAsync(c => c.Id == id);
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            if (charcter != null)
            {
               
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(charcter);
            }
            else
            {
                serviceResponse.Message = "Item not found";
                serviceResponse.Success = false;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharactor(UpdateCharactorDto updateCharactorDto)
        {
            var serviceRespones = new ServiceResponse<GetCharacterDto>();
          
            try
            {
                var updateCharacter = _db.Characters.FirstOrDefault(c => c.Id == updateCharactorDto.Id);
                if(updateCharacter != null)
                {
                    updateCharacter.Name = updateCharactorDto.Name;
                    updateCharacter.HitPoint = updateCharactorDto.HitPoint;
                    updateCharacter.Intelligence = updateCharactorDto.Intelligence;
                    updateCharacter.Strength = updateCharactorDto.Strength;
                    updateCharacter.Defence = updateCharactorDto.Defence;
                    updateCharacter.Class = updateCharactorDto.Class;
                    updateCharacter.DateUpdated = DateTime.Now;
                    await _db.SaveChangesAsync();
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
                Character removeCharacter = _db.Characters.First(c=>c.Id == id);
                if (removeCharacter != null)
                {
                    _db.Characters.Remove(removeCharacter);
                    await _db.SaveChangesAsync();
                    serviceRespones.Data = _db.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
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
                serviceRespones.Message = "Item not found";
                Console.WriteLine(ex.Message);
            }

            return serviceRespones;
        }
    }
}

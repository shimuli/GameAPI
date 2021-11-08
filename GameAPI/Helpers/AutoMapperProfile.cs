using AutoMapper;
using GameAPI.DTO.Characters;
using GameAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Helpers
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>().ReverseMap();

            CreateMap<Character, CreateCharactorDto>().ReverseMap();
            CreateMap<Character, UpdateCharactorDto>().ReverseMap();
        }

    }
}

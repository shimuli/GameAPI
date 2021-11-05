using GameAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Repository.IRepo
{
    public interface ICharacterRepo
    {
        List<Character> GetAllCharacters();
        Character GetOneCharacter(int id);

        List<Character> AddCharacter(Character newCharacter);
    }
}

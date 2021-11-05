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

        public List<Character> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return characters;
        }

        public List<Character> GetAllCharacters()
        {
            return characters;
        }

        public Character GetOneCharacter(int id)
        {
            var charcter = characters.FirstOrDefault(c => c.Id == id);
           
            return charcter;
        }
    }
}

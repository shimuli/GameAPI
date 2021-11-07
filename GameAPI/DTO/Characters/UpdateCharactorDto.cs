using GameAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.DTO.Characters
{
    public class UpdateCharactorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Ced";

        public int HitPoint { get; set; } = 10;

        public int Strength { get; set; } = 10;

        public int Defence { get; set; } = 10;

        public int Intelligence { get; set; } = 10;

        public RpgClass Class { get; set; } = RpgClass.Mage;
    }
}

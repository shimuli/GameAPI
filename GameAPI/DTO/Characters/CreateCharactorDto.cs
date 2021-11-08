using GameAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.DTO.Characters
{
    public class CreateCharactorDto
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public int HitPoint { get; set; }

        [Required]
        public int Strength { get; set; }

        [Required]
        public int Defence { get; set; }

        [Required]
        public int Intelligence { get; set; }


        [Required]
        public RpgClass Class { get; set; }
    }
}

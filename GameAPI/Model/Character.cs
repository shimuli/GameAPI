using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Model
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Ced";

        public int HitPoint { get; set; } = 10;

        public int Strength { get; set; } = 10;

        public int Defence { get; set; } = 10;

        public int Intelligence { get; set; } = 10;

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public RpgClass Class { get; set; } = RpgClass.Mage;

        // one to many-  one user can have many characters
        public User User { get; set; }
    }
}

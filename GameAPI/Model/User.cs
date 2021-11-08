using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

         public DateTime? LastLogin { get; set; }


        // one to many - one user can have many characters
        public List<Character> Characters { get; set; }
    }
}

using GameAPI.Helpers;
using GameAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Repository.IRepo
{
    public interface IAuthRepo
    {
        Task<ServiceResponse<int>> Register(User user, string password);

        Task<ServiceResponse<string>> Login(string username, string password);

        Task<bool> UserExists(string username);
    }
}

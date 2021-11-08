using AutoMapper;
using GameAPI.Data;
using GameAPI.Helpers;
using GameAPI.Model;
using GameAPI.Repository.IRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameAPI.Repository.Repo
{
    public class AuthRepo : IAuthRepo
    {
        private readonly IMapper _mapper;
        private readonly DataContext _db;
        private readonly IConfiguration _configuration;

        public AuthRepo(IMapper mapper, DataContext db, IConfiguration configuration)
        {
            _mapper = mapper;
            _db = db;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
            if(user == null)
            {
                response.Success = false;
                response.Message = "Invalid user!";              
            }
            else if(!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Invalid user! pp";

            }
            else
            {
                user.LastLogin = DateTime.Now;
                await _db.SaveChangesAsync();
                response.Data = UserToken(user);

            }

            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>();
            if(await UserExists(user.Username))
            {
                response.Success = false;
                response.Message = "Invalid User!";
                return response;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.DateCreated = DateTime.Now;
            user.DateUpdated = DateTime.Now;
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            response.Data = user.Id;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _db.Users.AnyAsync(x => x.Username.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
              
            return false;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPassword(string password,  byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i =0; i<computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string UserToken(User user)
        {
            var claim = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim(ClaimTypes.Name, user.Username),
               //new Claim(ClaimTypes.Role, userPhone.Role),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetting:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = cred
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(token);
        }


    }
}

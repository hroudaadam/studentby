using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestAPI.Entities;
using TestAPI.Helpers;
using TestAPI.Models;

namespace TestAPI.Services
{
    public interface IUserService
    {
        Task<AdminRegisterResponse> CreateAdminAsync(AdminRegisterRequest model);
        Task<UserAuthenticateResponse> AuthenticateAsync(UserAuthenticateRequest model);
        Task<User> CreateUserAsync(string email, string password, string role);
    }

    public class UserService : IUserService
    {
        private readonly StudentbyTestContext _context;
        private readonly AppSettings _appSettings;

        public UserService(StudentbyTestContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<User> CreateUserAsync(string email, string password, string role)
        {
            bool userExists = await _context.Users.AnyAsync(x => x.Email == email);
            if (userExists)
            {
                throw new StudentbyException("Uživatel již existuje");
            }

            byte[] hash, salt;
            HashPassword(password, out hash, out salt);

            var newUser = new User
            {
                Email = email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = role
            };
            return newUser;
        }

        private void HashPassword(string password, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<UserAuthenticateResponse> AuthenticateAsync(UserAuthenticateRequest model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
            {
                return null;
            }

            if (!VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            string token = GenerateJwtToken(user);

            return new UserAuthenticateResponse(user, token);
        }

        private bool VerifyPassword(string password, byte[] dbHash, byte[] dbSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(dbSalt))
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != dbHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /* ------------------------------------------------------------ */

        public async Task<AdminRegisterResponse> CreateAdminAsync(AdminRegisterRequest model)
        {
            if (await _context.Users.AnyAsync(x => x.Role == Role.Operator))
            {
                throw new StudentbyException("Admin already exists");
            }
            
            User user = await CreateUserAsync(model.Email, model.Password, Role.Operator);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AdminRegisterResponse(user);
        }      

        
    }
}

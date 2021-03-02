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
using WebAPI.Entities;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IUserService
    {
        Task<bool> EnsureOperatorAsync();
        Task<UserRes> AuthenticateAsync(UserReq model);
        Task<User> CreateAsync(string email, string password, string role);
        Task SetPassword(PasswordReq model);
    }

    /// <summary>
    /// Service for User operations
    /// </summary>
    public class UserService : IUserService
    {
        private readonly StudentbyContext _context;
        private readonly AppSettings _appSettings;

        public UserService(StudentbyContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <param name="role">User role</param>
        /// <returns>User entity</returns>
        public async Task<User> CreateAsync(string email, string password, string role)
        {
            bool userExists = await _context.Users.AnyAsync(x => x.Email == email);
            // user already exists (email not unique)
            if (userExists)
            {
                throw new AppLogicException("Uživatel již existuje");
            }

            // hash password
            byte[] hash, salt;
            HashPassword(password, out hash, out salt);

            // create user
            var newUser = new User
            {
                Email = email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = role
            };
            return newUser;
        }

        /// <summary>
        /// Hash password
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="hash">Hash</param>
        /// <param name="salt">Salt</param>
        private void HashPassword(string password, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="model">User DTO</param>
        /// <returns>User DTO</returns>
        public async Task<UserRes> AuthenticateAsync(UserReq model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == model.Email);

            // user not found
            if (user == null)
            {
                throw new AppLogicException("Špatný email nebo heslo");
            }

            // invalid password
            if (!VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new AppLogicException("Špatný email nebo heslo");
            }

            // generate token
            string token = GenerateJwt(user);
            return new UserRes(user, token);
        }

        /// <summary>
        /// Verify password
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="dbHash">Hash from DB</param>
        /// <param name="dbSalt">Salt from DB</param>
        /// <returns>Bool if password matches</returns>
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

        /// <summary>
        /// Generate JWT
        /// </summary>
        /// <param name="user">User entity</param>
        /// <returns>JWT</returns>
        private string GenerateJwt(User user)
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
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Create operator (ensure)
        /// </summary>
        /// <returns>Bool if operator was created</returns>
        public async Task<bool> EnsureOperatorAsync()
        {
            if (!await _context.Users.AnyAsync(us => us.Role == UserRoles.Operator))
            {
                User user = await CreateAsync("operator@abc.cz", "heslo123", UserRoles.Operator);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;            
        }  
        
        /// <summary>
        /// Set passord
        /// </summary>
        /// <param name="model">DTO password</param>
        /// <returns></returns>
        public async Task SetPassword(PasswordReq model)
        {
            var secretSplit = model.Secret.Split(':');
            if (secretSplit.Length != 2)
            {
                throw new AppLogicException("Nevalidní požadavek");
            }

            string modelHash = secretSplit[0];
            string email = secretSplit[1];

            var user = await _context.Users
                .Where(us => us.Email == email)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new AppLogicException("Daný uživatel neexistuje");
            }

            string dbHash = BitConverter.ToString(user.PasswordHash).Replace("-", "");
            bool validSecret = modelHash == dbHash;
            if (!validSecret)
            {
                throw new AppLogicException("Nevalidní kód");
            }

            byte[] hash, salt;
            HashPassword(model.Password, out hash, out salt);

            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            await _context.SaveChangesAsync();
        }
        
    }
}

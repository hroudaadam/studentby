using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Entities;
using TestAPI.Helpers;
using TestAPI.Models;

namespace TestAPI.Services
{
    public interface IUserService
    {
        Task<StudentRegisterResponse> CreateStudentAsync(StudentRegisterRequest model);
        Task<UserAuthenticateResponse> Authenticate(UserAuthenticateRequest model);
    }

    public class UserService : IUserService
    {
        private readonly StudentbyTestContext _context;

        public UserService(StudentbyTestContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<StudentRegisterResponse> CreateStudentAsync(StudentRegisterRequest model)
        {
            User user = await CreateUserAsync(model.Email, model.Password, Role.Student);

            Student student = new Student
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth.Value,
                User = user
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return new StudentRegisterResponse(student);
        }

        /// <summary>
        /// Creates new user object and validates values
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<User> CreateUserAsync(string email, string password, string role)
        {
            if (!IsValidEmail(email))
            {
                throw new AppException("Email is not valid");
            }

            bool userExists = await _context.Users.AnyAsync(x => x.Email == email);
            if (userExists)
            {
                throw new AppException("User already exists");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserAuthenticateResponse> Authenticate(UserAuthenticateRequest model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
            {
                throw new AppException("User does not exist");
            }

            if (!VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new AppException("Password is not valid");
            }

            string token = GenerateJwtToken();

            return new UserAuthenticateResponse(user, token);
        }

        /// <summary>
        /// Validates email adress
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <param name="salt"></param>
        private void HashPassword(string password, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="dbHash"></param>
        /// <param name="dbSalt"></param>
        /// <returns></returns>
        private bool VerifyPassword(string password, byte[] dbHash, byte[] dbSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(dbSalt))
            {
                byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
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
        /// 
        /// </summary>
        /// <returns></returns>
        private string GenerateJwtToken()
        {
            return "testtoken123";
        }
    }
}

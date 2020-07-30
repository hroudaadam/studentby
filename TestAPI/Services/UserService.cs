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
        Task<StudentRegisterResponse> CreateStudentAsync(StudentRegisterRequest model);
        Task<EmployeeRegisterResponse> CreateEmployeeAsync(EmployeeRegisterRequest model);
        Task<AdminRegisterResponse> CreateAdminAsync(AdminRegisterRequest model);
        Task<UserAuthenticateResponse> AuthenticateAsync(UserAuthenticateRequest model);
        Task<User> GetUserAsync(int userId);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<EmployeeRegisterResponse> CreateEmployeeAsync(EmployeeRegisterRequest model)
        {
            User user = await CreateUserAsync(model.Email, model.Password, Role.Employee);
            Company company = await _context.Companies.SingleOrDefaultAsync(x => x.CompanyId == model.CompanyId);

            Employee employee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                User = user,
                Company = company
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return new EmployeeRegisterResponse(employee);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AdminRegisterResponse> CreateAdminAsync(AdminRegisterRequest model)
        {
            if (await _context.Users.AnyAsync(x => x.Role == Role.Admin))
            {
                throw new AppException("Admin already exists");
            }
            
            User user = await CreateUserAsync(model.Email, model.Password, Role.Admin);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AdminRegisterResponse(user);
        }

        /// <summary>
        /// Creates new user object and validates values
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<User> CreateUserAsync(string email, string password, string role)
        {
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
        public async Task<UserAuthenticateResponse> AuthenticateAsync(UserAuthenticateRequest model)
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

            string token = GenerateJwtToken(user);

            return new UserAuthenticateResponse(user, token);
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
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
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
        /// 
        /// </summary>
        /// <returns></returns>
        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User> GetUserAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            return user;
        }
    }
}

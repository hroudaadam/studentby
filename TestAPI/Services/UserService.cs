using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Entities;
using TestAPI.Helpers;
using TestAPI.Models;

namespace TestAPI.Services
{
    public interface IUserService
    {
        Task<User> CreateAsync(string email, string password);
    }

    public class UserService : IUserService
    {
        private readonly StudentbyTestContext _context;

        public UserService(StudentbyTestContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates new user object and validates values
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> CreateAsync(string email, string password)
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

            string hashedPassword = password;
            var newUser = new User { Email = email, PasswordHash = hashedPassword };

            return newUser;
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

    }
}

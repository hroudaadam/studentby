using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Users.Exceptions;
using Studentby.Shared.Helpers;
using Studentby.Shared.Security;

namespace Studentby.App.Logic.Users;

internal static class UserCommonLogic
{
    public static async Task<User> CreateUserEntityAsync(
        string email,
        string password,
        UserRole role,
        IUserRepository userRepository,
        ICryptographyService cryptographyService)
    {
        if (await userRepository.AnyWithEmailAsync(email))
        {
            throw new UserEmailAlreadyTakenException();
        }

        (byte[] hash, byte[] salt) = cryptographyService.GetHashAndSalt(password);

        return new User()
        {
            Email = email,
            Role = role,
            PasswordHash = hash,
            PasswordSalt = salt
        };
    }

    public static string CreateChangePasswordSecret(User user)
    {
        Guard.NotNull(user);
        return $"{user.Email}:{BitConverter.ToString(user.PasswordHash).ToLower().Replace("-", "")}";
    }
}

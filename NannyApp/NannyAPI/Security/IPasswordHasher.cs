using NannyModels.Models.UserModels;

namespace NannyAPI.Security
{
    public interface IPasswordHasher
    {
        /// <summary>
        /// Given a new password, hash the password and return a password hash object.
        /// </summary>
        /// <param name="plainTextPassword">The password sent by the user</param>
        /// <returns>A hashed Password</returns>
        PasswordHash ComputeHash(string plainTextPassword);

        /// <summary>
        /// Verifies a match of an existing hashed password against a user input.
        /// </summary>
        /// <param name="existingHashedPassword">The exisiting password</param>
        /// <param name="plainTextPassword">The password given by the user</param>
        /// <param name="salt">The salt used comput the hash</param>
        /// <returns>If the user used the correct password</returns>
        bool VerifyHashMatch(string existingHashedPassword, string plainTextPassword, string salt);
    }
}

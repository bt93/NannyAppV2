using NannyAPI.Security.Models;
using System.Security.Cryptography;

namespace NannyAPI.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int _workFactor = 1000;
        private const int _salt = 8;
        private const int _keyBytes = 20;

        /// <inheritdoc />
        public PasswordHash ComputeHash(string plainTextPassword)
        {
            // Creates the hashing provider
            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(plainTextPassword, _salt, _workFactor);

            // Get the hashed password
            byte[] hash = deriveBytes.GetBytes(_keyBytes);

            // Set the salt value
            string salt = Convert.ToBase64String(deriveBytes.Salt);

            // Set the hashed password
            string hashedPassword = Convert.ToBase64String(hash);

            // Return the hashed password
            return new PasswordHash(hashedPassword, salt);
        }

        /// <inheritdoc />
        public bool VerifyHashMatch(string existingHashedPassword, string plainTextPassword, string salt)
        {
            // Convert salt to array
            byte[] saltArray = Convert.FromBase64String(salt);

            // Create the hashing provider
            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(plainTextPassword, saltArray, _workFactor);

            // Get the hashed password
            byte[] hash = deriveBytes.GetBytes(_keyBytes);

            // Convert the newPassword
            string newHashPassword = Convert.ToBase64String(hash);

            // Compare the password values
            return (existingHashedPassword == newHashPassword);
        }
    }
}

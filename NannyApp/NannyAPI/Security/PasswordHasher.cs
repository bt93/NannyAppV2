using NannyModels.Models.UserModels;
using System.Security.Cryptography;

namespace NannyAPI.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private int _workFactor;
        private int _salt;
        private int _keyBytes;

        public PasswordHasher(int workFactor, int salt, int keyBytes)
        {
            _workFactor = workFactor;
            _salt = salt;
            _keyBytes = keyBytes;
        }

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

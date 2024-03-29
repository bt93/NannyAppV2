﻿namespace NannyModels.Models.UserModels
{
    public class PasswordHash
    {
        /// <summary>
        /// Creates a new hashed password
        /// </summary>
        public PasswordHash() { }

        /// <summary>
        /// Creates a new hashed password.
        /// </summary>
        /// <param name="password">The hashed password</param>
        /// <param name="salt">The salt used to get the hashed password.</param>
        public PasswordHash(string password, string salt)
        {
            Password = password;
            Salt = salt;
        }

        /// <summary>
        /// The hashed password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The salt used to get the hashed password.
        /// </summary>
        public string Salt { get; set; }
    }
}